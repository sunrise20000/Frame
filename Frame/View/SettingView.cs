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

namespace Frame.View
{
    public partial class SettingView : CommandActionBase
    {
        public SettingView()
        {
            InitializeComponent();
        }
        protected override void OnRecvMessage<Msg2>(Msg2 msg)
        {

        }
    }
}
