using Frame.Model;
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
        List<MessageUserControl> ControlList = new List<MessageUserControl>();

        HistoryView historyView = new HistoryView();
        SettingView settingView = new SettingView();
        LogoutView logoutView = new LogoutView();

       

        public Form1()
        {
            InitializeComponent();
            InitCtrl();
            LoadConfig();
        }


        private void InitCtrl()
        {
            historyView.Dock = System.Windows.Forms.DockStyle.Fill;
            settingView.Dock = System.Windows.Forms.DockStyle.Fill;

            Controls.Add(historyView);
            Controls.Add(settingView);

            ControlList.Add(historyView);
            ControlList.Add(settingView);
            ControlList.Add(homeView1);

            //绑定消息
            settingView.AddMonitorList(homeView1);
            historyView.AddMonitorList(homeView1);

            //
            homeView1.AddMonitorList(settingView);


        }

        private void LoadConfig()
        {
            var station = new StationTest();
            StationMgr.Instance.AddStation(station.GetType().Name,station);
            homeView1.AddMonitorList(station);
        }
        private void barButtonItemHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControl(homeView1);
        }

        private void barButtonItemSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControl(settingView);
        }

        private void barButtonItemHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControl(historyView);
        }

        private void ShowUserControl(MessageUserControl Ctrl)
        {
            if (ControlList.Contains(Ctrl))
            {
                foreach (var w in ControlList.Where(u => u != Ctrl))
                    w.Visible = false;
                Ctrl.Visible = true;
            }
        }

        private void barButtonItemStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StationMgr.Instance.StartAllStation();
        }

        private void barButtonItemStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StationMgr.Instance.StopAllStation();
        }
    }
}
