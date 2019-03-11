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

namespace Frame.View
{
    public partial class HomeView : CommandActionBase
    {
        public HomeView()
        {
            InitializeComponent();
        }
        protected override void OnRecvMessage<Msg1>(Msg1 msg)
        {

        }
    }
}
