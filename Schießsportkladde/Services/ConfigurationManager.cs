using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schießsportkladde.Services
{
    public static class ConfigurationManager
    {
        public static JObject LoadThemeConfig(int ?theme = 1)
        {
            string themePath;

            switch (theme)
            {
                case 1:
                    themePath = "Styles/themeConfig.json";
                    break;

                default:
                    themePath = "Styles/themeConfig.json";
                    break;
            }
            if (File.Exists(themePath))
            {
                string config = File.ReadAllText(themePath);
                return JObject.Parse(config);
            }
            return null;
        }
    }
}
