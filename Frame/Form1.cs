using Frame.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frame
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        HistoryView ViewHistory = new HistoryView();
        SettingView ViewSetting = new SettingView();
        public Form1()
        {
            InitializeComponent();

            ViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            ViewSetting.Dock = System.Windows.Forms.DockStyle.Fill;

            Controls.Add(ViewHistory);
            Controls.Add(ViewSetting);

            ViewSetting.AddMonitorList(homeView1);
            ViewHistory.AddMonitorList(homeView1);
        }

        private void barButtonItemHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ViewHistory.Visible = false;
            ViewSetting.Visible = false;
            homeView1.Visible = true;
        }

        private void barButtonItemSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ViewHistory.Visible = false;
            ViewSetting.Visible = true;
            homeView1.Visible = false;
        }

        private void barButtonItemHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ViewHistory.Visible = true;
            ViewSetting.Visible = false;
            homeView1.Visible = false;
        }
    }
}
