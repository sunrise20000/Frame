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

        HomeView homeView = new HomeView();
        HistoryView historyView = new HistoryView();
        SettingView settingView = new SettingView();

       

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
            homeView.Dock= System.Windows.Forms.DockStyle.Fill;

            Controls.Add(homeView);
            Controls.Add(historyView);
            Controls.Add(settingView);
            

            ControlList.Add(homeView);
            ControlList.Add(historyView);
            ControlList.Add(settingView);
            

            //绑定homeView发出的消息
            settingView.AddMonitorList(homeView);
            historyView.AddMonitorList(homeView);


            //绑定settingView发出的消息
            homeView.AddMonitorList(settingView);


        }

        private void LoadConfig()
        {
            var station = new StationTest();
            var station1 = new StationTest1();
            var station2 = new StationTest2();
            StationMgr.Instance.AddStation(station);
            StationMgr.Instance.AddStation(station1);
            StationMgr.Instance.AddStation(station2);

            homeView.AddMonitorList(station);
            homeView.AddMonitorList(station1);
            homeView.AddMonitorList(station2);

            homeView.SetStationBinding(station, station1, station2);

        }
        private void barButtonItemHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControl(homeView);
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StationMgr.Instance.StopAllStation();
            StationMgr.Instance.WaitFinish();
       
        }
    }
}
