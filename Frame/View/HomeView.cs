﻿using System;
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
    public partial class HomeView : MessageUserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        public override void OnRecvMessage<Msg1>(Msg1 msg)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage(new Msg1() {  ID="Frome HomeView"});
        }
    }
}
