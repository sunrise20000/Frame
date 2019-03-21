using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config.HardwareCfg.InstrumentCfg
{
    public enum EnumConnectType
    {
        Comport,
        Ethernet,
    }
    public class InstrumentCfgBase
    {
        private EnumConnectType connectMode = EnumConnectType.Comport;

        public bool Enable { get; set; }

        public string Name { get; set; }

        public string ConnectType
        {
            get { return connectMode.ToString(); }
            set
            {
                Enum.TryParse(value, out connectMode);
            }
        }

        public string PortName { get; set; }
    }
}
