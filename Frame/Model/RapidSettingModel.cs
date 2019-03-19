using Frame.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Model
{
    public class RapidSettingModel
    {
        [CategoryAttribute("工艺参数"), DescriptionAttribute("Para1")]
        [RapidSettingAttribute("")]
        public int Para1 { get; set; } = 20;


        [RapidSettingAttribute("")]
        [CategoryAttribute("工艺参数"), DescriptionAttribute("Para2")]
        public int Para2 { get; set; } = 78;


        [RapidSettingAttribute("")]
        [CategoryAttribute("工艺参数"), DescriptionAttribute("Para3")]
        public int Para3 { get; set; }
     
    }
}
