using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using SerializzazioneFwf.Attributi;

namespace FwfTest
{
    [LineaFwf(dimensione: 51)]
    internal class Record1
    {
        [CampoFwf(inizio: 0, dimensione: 20)]
        public string TRA_CAMPO_1 { get; set; }

        [GruppoFwf(inizio: 20, dimensione: 10, elementiMassimi: 3)]
        public List<R11> TRA_CAMPO_2 { set; get; }

        [CampoFwf(inizio: 50, dimensione: 1)]
        public string TRA_CAMPO_3 { get; set; }

        //[CampoFwf(inizio: 50, dimensione: 10000-50)]
        //public string FILL {get; set;}
    }

    internal class R11
    {
        [CampoFwf(inizio: 0, dimensione: 5)]
        public string TRA_CAMPO_2_1 { get; set; }

        [CampoFwf(inizio: 5, dimensione: 3)]
        public string TRA_CAMPO_2_2 { get; set; }
    }
}
