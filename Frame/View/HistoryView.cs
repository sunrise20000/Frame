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

        public override void OnRecvMessage<T>(T msg)
        {
            if (msg is Msg1)
            {
                MessageBox.Show((msg as Msg1).ID+"sfsfsfs");
            }
        }
    }
}
