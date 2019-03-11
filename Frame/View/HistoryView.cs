using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Frame.Interface;
using Frame.Class;

namespace Frame.View
{
    public partial class HistoryView : MessageUserControl
    {
        public HistoryView()
        {
            InitializeComponent();
            
        }

        public void OnMsg1(Msg1 msg)
        {
            MessageBox.Show(msg.ID);
        }
        public void OnMsg2(Msg2 msg)
        {
            MessageBox.Show("HistoryView"+msg.dt.GetDateTimeFormats()[35]);
        }
    }
}
