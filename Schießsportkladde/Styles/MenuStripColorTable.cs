using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schießsportkladde.Services;
using System.Threading.Tasks;

namespace Schießsportkladde.Styles
{
    public class MenuStripColorTable : ProfessionalColorTable
    {
        JObject themeConfig = ConfigurationManager.LoadThemeConfig();
        public override Color MenuItemSelected
        {
            get { return ColorTranslator.FromHtml((string)themeConfig["primaryCbxBgr"]); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return ColorTranslator.FromHtml((string)themeConfig["primaryCbxBgr"]); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return ColorTranslator.FromHtml((string)themeConfig["primaryCbxBgr"]); }
        }
        public override Color MenuItemBorder
        {
            get { return ColorTranslator.FromHtml((string)themeConfig["primaryCbxBgr"]); }

        }
    }
}
