using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RapidSettingAttribute : Attribute
    {
        public RapidSettingAttribute(string DisplayName,bool IsForRapidSetting=true)
        {
            this.Display = Display;
            this.IsForRapidSetting = IsForRapidSetting;
        }

        public string Display { get; set; }
        public bool IsForRapidSetting { get; set; }
    }
}
