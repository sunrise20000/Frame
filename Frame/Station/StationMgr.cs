using Frame.Class;
using Frame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Frame.Model
{
    public class StationMgr : Singleton<StationMgr>,IManagable<StationBase>
    {
        public Dictionary<string, StationBase> Dic { get; set; } = new Dictionary<string, StationBase>();
        public bool StartAllStation()
        {
            bool bRet = true;
            foreach (var it in Dic)
                bRet&=it.Value.Start();
            return bRet;
        }
        public bool StopAllStation()
        {
            bool bRet = true;
            foreach (var it in Dic)
                bRet &= it.Value.Stop();
            return bRet;
        }
        public bool PuseAllStation()
        {
            bool bRet=true;
            foreach (var it in Dic)
                bRet &= it.Value.Puse();
            return bRet;
        }
        public bool ResumeAllStation()
        {
            bool bRet = true;
            foreach (var it in Dic)
                bRet &= it.Value.Resume();
            return bRet;
        }

        public void WaitFinish()
        {
            foreach (var it in Dic)
                it.Value.Wait();
        }

        public void AddInstanse(StationBase ins)
        {
            if (ins == null)
                return;
            foreach (var it in Dic)
            {
                if (it.Key == ins.GetType().Name)
                    return;
            }
            Dic.Add(ins.GetType().Name, ins);
        }

        public StationBase FindInstanseByName(string strName)
        {
            if (strName == null)
                return null;
            foreach (var it in Dic)
                if (it.Key == strName)
                    return it.Value;
            return null;
        }

        public StationBase FindInstanseByIndex(int Index)
        {
            if (Index < Dic.Count)
                return Dic.ElementAt(Index).Value;
            return null;
        }
    }
}
