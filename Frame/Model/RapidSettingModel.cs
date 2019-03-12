using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Model
{
    public class RapidSettingModel
    {
        [CategoryAttribute("工艺参数"), DescriptionAttribute("Para1")]
        public int Para1 { get; set; } = 20;
        [CategoryAttribute("工艺参数"), DescriptionAttribute("Para2")]
        public int Para2 { get; set; } = 78;
    }
}
