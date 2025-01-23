using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class SchützenName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SchützenName(int id, string name) {
            Id = id;
            Name = name;
        }

        public SchützenName() { }
    }
}
