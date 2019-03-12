using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Frame.Model
{
    public class StationMgr
    {
        private Dictionary<string,StationBase> stationDic=new Dictionary<string, StationBase>();
        private StationMgr() { }
        private static readonly Lazy<StationMgr> _instance = new Lazy<StationMgr>(() => new StationMgr());
        public static StationMgr Instance
        {
            get { return _instance.Value; }
        }
        public void AddStation(string stationName,StationBase station)
        {
            if (stationName == null || station == null)
                return;
            foreach (var it in stationDic)
            {
                if (it.Key == stationName)
                    return;
            }
            station.StationIndex = stationDic.Count;
            stationDic.Add(stationName, station);
        }
        public StationBase FindStationByName(string strName)
        {
            if (strName == null)
                return null;
            foreach(var it in stationDic)
                if (it.Key == strName)
                    return it.Value;
            return null;
        }
        public StationBase FindStationByIndex(int index)
        {
            if (index<0 || index> stationDic.Count)
                return null;
            foreach (var it in stationDic)
                if (it.Value.StationIndex == index)
                    return it.Value;
            return null;
        }
        public bool StartAllStation()
        {
            bool bRet = true;
            foreach (var it in stationDic)
                bRet&=it.Value.Start();
            return bRet;
        }
        public bool StopAllStation()
        {
            bool bRet = true;
            foreach (var it in stationDic)
                bRet &= it.Value.Stop();
            return bRet;
        }
        public bool PuseAllStation()
        {
            bool bRet=true;
            foreach (var it in stationDic)
                bRet &= it.Value.Puse();
            return bRet;
        }
        public bool ResumeAllStation()
        {
            bool bRet = true;
            foreach (var it in stationDic)
                bRet &= it.Value.Resume();
            return bRet;
        }

    }
}
