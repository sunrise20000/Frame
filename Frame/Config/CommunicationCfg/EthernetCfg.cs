using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config.CommunicationCfg
{
    public enum EnumTcpMode
    {
        Server,
        Client,
    }
    public class EthernetCfg
    {
        private EnumTcpMode mode = EnumTcpMode.Client;

        public bool Enabled { get; set; }
        public string PortName { get; set; }
        public string Mode
        {
            get { return mode.ToString(); }
            set {
                Enum.TryParse(value, out mode);
            }
        }
        public string IP { get; set; }
        public int Port
        {
            get;set;
        }
        public string EOL { get; set; }
        public double TimeOut
        {
            get;set;
        }
    }
}
