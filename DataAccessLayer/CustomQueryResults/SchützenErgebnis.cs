using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class SchützenErgebnis
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Datum { get; set; }
        public string Waffe { get; set; }
        public int Ergebnis { get; set; }
        public SchützenErgebnis(int id, string name, string datum, string waffe, int ergebnis)
        {
            Id = id;
            Name = name;
            Datum = datum;
            Waffe = waffe;
            Ergebnis = ergebnis;
        }

        public SchützenErgebnis() { }
    }
}
