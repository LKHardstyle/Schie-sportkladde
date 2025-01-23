using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class JoinedSchütze
    {
        public int Id { get; set; }
        public string SName { get; set; }
        public string Geburtsdatum { get; set; }
        public string StraßeNr { get; set; }
        public string PLZOrt { get; set; }
        public string TelefonNr { get; set; }
        public string Email { get; set; }
        public string VBName { get; set; }
        public string MitgliedsNrVerband { get; set; }
        public string EintrittVerband { get; set; }
        public string VEName { get; set; }
        public string MitgliedsNrVerein { get; set; }
        public string EintrittVerein { get; set; }

        public JoinedSchütze(string name, string date, string straßeNr, string plzort, string telefonNr, string eMail, string verband, string mitgliedsNrVerband, string eintrittVerband, string verein, string mitgliedsNrVerein, string eintrittVerein)
        {
            SName = name;
            Geburtsdatum = date;
            StraßeNr = straßeNr;
            PLZOrt = plzort;
            TelefonNr = telefonNr;
            Email = eMail;
            VBName = verband;
            MitgliedsNrVerband = mitgliedsNrVerband;
            EintrittVerband = eintrittVerband;
            VEName = verein;
            MitgliedsNrVerein = mitgliedsNrVerein;
            EintrittVerein = eintrittVerein;
        }

        public JoinedSchütze() { }
    }
}
