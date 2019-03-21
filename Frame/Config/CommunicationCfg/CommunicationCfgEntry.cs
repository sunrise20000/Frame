using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config.CommunicationCfg
{
    public class CommunicationCfgEntry
    {
        public ComportCfg[] Comports {get;set;}
        public EthernetCfg[] Ethernets { get; set; }
    }
}
