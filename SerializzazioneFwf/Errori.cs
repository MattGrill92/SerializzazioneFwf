using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescrittoriFlatFile
{
    internal class Errori
    {
        public static void LanciaEccezioneSe(bool ok, string messaggio, params object[] args)
            => LanciaEccezioneSe<Exception>(ok, messaggio, args);
        public static void LanciaEccezioneSe<T>(bool ok, string messaggio, params object[] args) where T : Exception, new()
        {
            if (ok)
                return;

            var eccezione = 
                (T)Activator.CreateInstance(typeof(T), string.Format(messaggio, args));
            
            throw eccezione;
        }

        public const string NON_INIZIALIZZATO = "";
    }
}
