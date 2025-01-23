using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Kladde
    {
        public int Id { get; set; }
        public int SchützeId { get; set; }
        public string Datum { get; set; }
        public int WettbewerbId { get; set; }
        public int WaffeId { get; set; }       
        public int Schusszahl { get; set; }
        public int SchießstandId { get; set; }
        public int Ergebnis { get; set; }
        public int AufsichtId { get; set; }

        public Kladde(int id, int schützeId,string datum, int wettbewerbId, int waffeId, int schusszahl, int schießstandId, int ergebnis, int aufsichtId)
        {
            Id = id;
            SchützeId = schützeId;
            Datum = datum;
            WettbewerbId = wettbewerbId;
            WaffeId = waffeId;
            Schusszahl = schusszahl;
            SchießstandId = schießstandId;
            Ergebnis = ergebnis;
            AufsichtId = aufsichtId;
        }

        public Kladde() { }
    }
}
