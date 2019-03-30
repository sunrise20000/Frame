using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Instrument
{
    public class InstrumentBase<T,K> where T: InstrumentCfgBase where K: CommunicationCfgBase
    {
        protected T InstrumentCfg { get; set; }
        protected K CommunicationCfg { get; set; }
        public InstrumentBase(T Instrumetcfg, K CommunicationCfg)
        {
            this.InstrumentCfg = Instrumetcfg;
            this.CommunicationCfg = CommunicationCfg;
            InstrumentName = InstrumentCfg.Name;
        }

        public string InstrumentName { get; set; }
    }
}
