using Frame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frame.View
{
    /// <summary>
    /// UC_StationInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_StationInfo : UserControl
    {
        public UC_StationInfo()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// 为ShowInfo绑定消息源，有几个Station就生成几个ObservableCollection
        /// </summary>
        /// <param name="stations"></param>
        public void SetStationList(params StationBase[] stations)
        {
            foreach (var it in stations)
            {
                InfoList.Add(new StationInfoModel() {
                    StationName=it.StationName, 
                });
            }
        }

        public void ShowInfo(string msg,string stationName, int maxCount=5)
        {
            var modellist = InfoList.Where(m => m.StationName.Equals(stationName)).FirstOrDefault();
            if (modellist != null)
            {
                modellist.InfoCollect.Add(msg);
                while (modellist.InfoCollect.Count > maxCount)
                    modellist.InfoCollect.RemoveAt(0);
            }
        }

        public void ShowInfo(string msg, StationBase station, int maxCount = 5)
        {
            var modellist = InfoList.Where(m => m.StationName.Equals(station.StationName)).FirstOrDefault();
            if (modellist != null)
            {
                modellist.InfoCollect.Add(msg);
                while (modellist.InfoCollect.Count > maxCount)
                    modellist.InfoCollect.RemoveAt(0);
            }
        }

        public ObservableCollection<StationInfoModel> InfoList { get; set; } = new ObservableCollection<StationInfoModel>();  
    }
}
