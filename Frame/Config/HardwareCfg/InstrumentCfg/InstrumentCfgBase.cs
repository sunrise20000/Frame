using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config.HardwareCfg.InstrumentCfg
{
    public class InstrumentCfgBase
    {
        public bool Enable { get; set; }

        public string Name { get; set; }

        public EnumConnectType ConnectType { get; set; }

        public string PortName { get; set; }
    }
}
