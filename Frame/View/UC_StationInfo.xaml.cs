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
            InfoList.Add(new StationInfoModel() {
                StationName="Stationname",
            });
        }
        public ObservableCollection<StationInfoModel> InfoList { get; set; } = new ObservableCollection<StationInfoModel>();  
    }
}
