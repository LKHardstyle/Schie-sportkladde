using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcceessLayer
{
    static internal class ConnectionHelper
    {       
        internal static string ConnectionString {
            get
            {
                return ConfigurationManager.ConnectionStrings
                ["KladdeConnectionString"].ConnectionString;
            }
        }
    }
}
