using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Schütze
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Geburtsdatum { get; set; }
        public string StraßeNr { get; set; }
        public string PLZOrt { get; set; }
        public string TelefonNr { get; set; }
        public string Email { get; set; }
        public int VerbandId { get; set; }
        public string MitgliedsNrVerband { get; set; }
        public string EintrittVerband { get; set; }
        public int VereinId { get; set; }
        public string MitgliedsNrVerein { get; set; }
        public string EintrittVerein { get; set; }

        public Schütze(string name, string date, string straßeNr, string plzort, string telefonNr, string eMail, int verbandId, string mitgliedsNrVerband, string eintrittVerband, int vereinId, string mitgliedsNrVerein, string eintrittVerein)
        {
            Name = name;
            Geburtsdatum = date;
            StraßeNr = straßeNr;
            PLZOrt = plzort;
            TelefonNr = telefonNr;
            Email = eMail;
            VerbandId = verbandId;
            MitgliedsNrVerband = mitgliedsNrVerband;
            EintrittVerband = eintrittVerband;
            VereinId = vereinId;
            MitgliedsNrVerein = mitgliedsNrVerein;
            EintrittVerein = eintrittVerein;
        }

        public Schütze() { }
    }
}
