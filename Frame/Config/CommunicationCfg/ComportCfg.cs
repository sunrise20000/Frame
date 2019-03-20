using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config.CommunicationCfg
{
    public enum EnumParity
    {
        Even,
        Mark,
        None,
        Odd,
        Space,
    }
    public enum EnumStopBits
    {
        None,
        One,
        OnePointFive,
        Two,
    }
    public class ComportCfg
    {
        private EnumParity parity = EnumParity.None;
        private EnumStopBits stopBits = EnumStopBits.One;
        public bool Enabled { get; set; }
        public string PortName { get; set; }
        public string Port { get; set; }
        public int Baudrate { get; set; }
        public string Parity
        {
            get {
                return parity.ToString();
            }
            set {
                Enum.TryParse(value, out parity);
            }
        }
        public int DataBits { get; set; }
        public string StopBit
        {
            get { return stopBits.ToString(); }
            set {
                Enum.TryParse(value, out stopBits);
            }
        }

        
    }
}
