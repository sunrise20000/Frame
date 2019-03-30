using Frame.Class;
using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using Frame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Instrument
{
    public class InstrumentMgr<T, K> :Singleton<InstrumentMgr<T, K>>, IManagable<InstrumentBase<T, K>> where T : InstrumentCfgBase where K : CommunicationCfgBase
    {
        public Dictionary<string, InstrumentBase<T, K>> Dic { get; set; } = new Dictionary<string, InstrumentBase<T, K>>();

        public void AddInstanse(InstrumentBase<T, K> ins)
        {
            if (ins == null)
                return;
            foreach (var it in Dic)
            {
                if (it.Key == ins.InstrumentName)
                    return;
            }
            Dic.Add(ins.InstrumentName, ins); ;
        }

        public InstrumentBase<T, K> FindInstanseByIndex(int Index)
        {
            if (Index < Dic.Count)
                return Dic.ElementAt(Index).Value;
            return null;
        }

        public InstrumentBase<T, K> FindInstanseByName(string strName)
        {
            if (strName == null)
                return null;
            foreach (var it in Dic)
                if (it.Key == strName)
                    return it.Value;
            return null;
        }
    }
}
