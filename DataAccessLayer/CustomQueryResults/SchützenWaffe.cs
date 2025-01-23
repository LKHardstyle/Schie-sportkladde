using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class SchützenWaffe
    {
        public int Weaponused { get; set; }
        public int SchützeId { get; set; }
        public string Waffe { get; set; }        
        public SchützenWaffe(int weaponused ,int schützeid, string waffe)
        {
            Weaponused = weaponused;
            SchützeId = schützeid;
            Waffe = waffe;            
        }
        public SchützenWaffe() { }
    }
}
