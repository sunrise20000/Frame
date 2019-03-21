using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Instrument
{
    public class InstrumentBase<T,K> where T: InstrumentCfgBase where K: class
    {
        protected InstrumentCfgBase Cfg { get; set; }
        public InstrumentBase(InstrumentCfgBase cfg)
        {
            Cfg = cfg;
        }

        

    }
}
