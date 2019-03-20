using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config
{
    public enum EnumConnectType
    {
        Comport,
        Ethernet,
        GPIB,
    }
    public class InstrumentConfigBase
    {
        private EnumConnectType connectType = EnumConnectType.Comport;

        /// <summary>
        ///是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 仪器的名称
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// 连接方式
        /// </summary>
        public string ConnectMode
        {
            get { return connectType.ToString(); }
            set {
                Enum.TryParse(value, out connectType);
            }
        }

        /// <summary>
        /// 通信接口的名称
        /// </summary>
        public string PortName { get; set; }
    }
}
