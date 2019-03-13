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
    /// UC_HistoryPanel.xaml 的交互逻辑
    /// </summary>
    public partial class UC_HistoryPanel : UserControl
    {
        public UC_HistoryPanel()
        {
            InitializeComponent();
        }
        public ObservableCollection<MessageModel> MsgCollect { get; set; } = new ObservableCollection<MessageModel>();
    }
}
