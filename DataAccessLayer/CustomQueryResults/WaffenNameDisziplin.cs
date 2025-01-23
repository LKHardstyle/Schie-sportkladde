using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class WaffenNameDisziplin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Disziplin { get; set; }

        public WaffenNameDisziplin(int id, string name, string disziplin)
        {
            Id = id;
            Name = name;
            Disziplin = disziplin;
        }
        public WaffenNameDisziplin() { }
    }
}
