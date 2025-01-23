using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Waffe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kaliber { get; set; }
        public string Disziplin { get; set; }        
    }
}
