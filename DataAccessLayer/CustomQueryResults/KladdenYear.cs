using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class KladdenYear
    {        
        public string Date { get; set; }

        public KladdenYear(string date)
        {            
            Date = date;
        }

        public KladdenYear() { }
    }
}
