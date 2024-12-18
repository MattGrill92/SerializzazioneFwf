using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializzazioneFwf.Attributi
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LineaFwf : Attribute
    {
        public int Dimensione;
        public LineaFwf(int dimensione)
        {
            Dimensione = dimensione;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CampoFwf : Attribute
    {
        public int Inizio;
        public int Dimensione;
        public CampoFwf(int inizio, int dimensione)
        {
            Inizio = inizio;
            Dimensione = dimensione;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GruppoFwf : Attribute
    {
        public int Inizio;
        public int Dimensione;
        public int ElementiMassimi;
        public GruppoFwf(int inizio, int dimensione, int elementiMassimi)
        {
            Inizio = inizio;
            Dimensione = dimensione;
            ElementiMassimi = elementiMassimi;
        }
    }
}
