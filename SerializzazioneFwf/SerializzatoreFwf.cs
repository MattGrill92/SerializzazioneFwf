using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DescrittoriFlatFile;
using SerializzazioneFwf.Attributi;

namespace SerializzazioneFwf
{
    public static class SerializzatoreFwf
    {
        private static void SetCampo(char[] sb, CampoFwf campo, string valore, int offset)
        {
            sb.ReplaceAt(campo.Inizio + offset, campo.Dimensione, valore, Costanti.CARATTERE_VUOTO);
        }

        public static string Serializza<T>(IEnumerable<T> lista, string terminatore = "\n")
        {
            int dimensione = 0;
            var oggetti = new List<(object Oggetto, int Inizio, int Fine)>();

            foreach(var ogg in lista)
            {
                var tipoLinea = ogg.GetType();
                var attrLinea = tipoLinea.GetCustomAttributes(typeof(LineaFwf), false).Cast<LineaFwf>().FirstOrDefault();

                if (attrLinea == null)
                    throw new Exception("L'oggetto passato non implementa l'attributo " + nameof(LineaFwf));

                oggetti.Add((ogg, dimensione, dimensione + attrLinea.Dimensione));

                dimensione += attrLinea.Dimensione + terminatore.Length;
            }

            var sb = new char[dimensione];
            sb.Populate(Costanti.CARATTERE_VUOTO);

            Parallel.ForEach(oggetti, (oggetto) =>
            {
                if (oggetto.Inizio > 0)
                {
                    sb.ReplaceAt(oggetto.Inizio - terminatore.Length, terminatore.Length, terminatore);
                }
            
                _ = Serializza(sb, oggetto.Oggetto, oggetto.Inizio, false);
            });

            return new string(sb);
        }

        public static string Serializza(object ogg)
            => Serializza(null, ogg, 0, true);
        private static string Serializza(char[] sb, object ogg, int offset, bool restituisci)
        {
            var tipoLinea = ogg.GetType();
            var attrLinea = tipoLinea.GetCustomAttributes(typeof(LineaFwf), false).Cast<LineaFwf>().FirstOrDefault();

            if (attrLinea == null)
                throw new Exception("L'oggetto passato non implementa l'attributo " + nameof(LineaFwf));

            if (sb == null)
            {
                sb = new char[attrLinea.Dimensione];
                sb.Populate(Costanti.CARATTERE_VUOTO);
            }

            var stackGruppi = new Stack<(object Oggetto, int Offset)>();
            stackGruppi.Push((ogg, 0));

            for(int i = 0; i < Costanti.PROFONDITA_MASSIMA_GRUPPI; i++)
            {
                if (stackGruppi.Count == 0)
                    break;

                var gruppo = stackGruppi.Pop();
                var tipo = gruppo.Oggetto.GetType();

                var propsCampoGruppo = tipo.GetProperties();

                foreach (var propCampoGruppo in propsCampoGruppo)
                {
                    // Campi
                    {
                        var attrCampo = propCampoGruppo.GetCustomAttributes(typeof(CampoFwf), false).Cast<CampoFwf>().FirstOrDefault();

                        if (attrCampo != null)
                        {
                            SetCampo(sb, attrCampo, (string)propCampoGruppo.GetValue(gruppo.Oggetto, null), gruppo.Offset + offset);
                            continue;
                        }
                    }

                    // Gruppi
                    {
                        var attrGruppo = propCampoGruppo.GetCustomAttributes(typeof(GruppoFwf), false).Cast<GruppoFwf>().FirstOrDefault();

                        if (attrGruppo != null)
                        {
                            int k = 0;
                            foreach (var campoGruppo in (IEnumerable)propCampoGruppo.GetValue(gruppo.Oggetto, null))
                            {
                                stackGruppi.Push((campoGruppo, attrGruppo.Inizio + gruppo.Offset + attrGruppo.Dimensione * k++));
                            }
                            continue;
                        }
                    }
                }
            }

            return restituisci ? new string(sb) : null;
        }
    }

    
}
