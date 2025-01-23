using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class JoinedKladde
    {
        public int Id { get; set; }        
        public string Schütze { get; set; }
        public string Datum { get; set; }
        public string Wettbewerb { get; set; }
        public string Waffe { get; set; } 
        public string Kaliber { get; set; }
        public int Schusszahl { get; set; }
        public int Ergebnis { get; set; }
        public string Disziplin { get; set; }
        public string Schießstand { get; set; }
        public string Aufsicht { get; set; }
    }
}
