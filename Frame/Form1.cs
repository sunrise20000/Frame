using Frame.Class.ViewCommunicationMessage;
using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using Frame.Definations;
using Frame.Instrument;
using Frame.Interface;
using Frame.Model;
using Frame.View;
using FXPLCCommunicationLib;
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
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm , ICommandAction
    {
        List<MessageUserControl> ControlList = new List<MessageUserControl>();

        HomeView homeView = new HomeView();
        HistoryView historyView = new HistoryView();
        SettingView settingView = new SettingView();
        CameraSetting settingCamView = new CameraSetting();
        EnumSystemState systemState = EnumSystemState.IDLE;
        
        public List<ICommandAction> ReceiverList { get; set; } = new List<ICommandAction>();

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
            settingCamView.Dock= System.Windows.Forms.DockStyle.Fill;


            Controls.Add(homeView);
            Controls.Add(historyView);
            Controls.Add(settingView);
            Controls.Add(settingCamView);

            ControlList.Add(homeView);
            ControlList.Add(historyView);
            ControlList.Add(settingView);
            ControlList.Add(settingCamView);


            //关注homeView发出的消息
            settingView.AddMonitorList(homeView);
            historyView.AddMonitorList(homeView);
            this.AddMonitorList(homeView);



            //关注settingView发出的消息
            homeView.AddMonitorList(settingView);


            barCheckItem2.Caption = barCheckItem2.Checked ? "Manual" : "Auto";
            UpdateButtonState();
        }

        private void LoadConfig()
        {
            var station = new StationTest();
            var station1 = new StationReadAlarm();
            var station2 = new StationTest2();

            StationMgr.Instance.AddInstanse(station);
            StationMgr.Instance.AddInstanse(station1);
            StationMgr.Instance.AddInstanse(station2);

            homeView.AddMonitorList(station,station1,station2);
            historyView.AddMonitorList(station, station1, station2);

            //可以接收Station的ShowInfo消息
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

        private void barButtonItemCamera_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowUserControl(settingCamView);
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
            if (StationMgr.Instance.StartAllStation())
            {
                SystemState = EnumSystemState.RUN;
            }
        }

        private void barButtonItemStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (StationMgr.Instance.StopAllStation())
            {
                SystemState = EnumSystemState.IDLE;
            }
           
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StationMgr.Instance.StopAllStation();
            StationMgr.Instance.WaitFinish();
       
        }

        public void SendMessage<T>(T msg, ICommandAction Receive = null) where T : ViewMessageBase
        {
            if (Receive == null)
            {
                foreach (var it in ReceiverList)
                    it.OnRecvMessage(msg);
            }
            else
            {
                Receive.OnRecvMessage(msg);
            }
        }

        /// <summary>
        /// 添加监视窗体
        /// </summary>
        /// <param name="MonitorCtrl">要监视的窗体</param>
        public void AddMonitorList(ICommandAction MonitorCtrl)
        {
            if (!MonitorCtrl.ReceiverList.Contains(this))
                MonitorCtrl.ReceiverList.Add(this);
        }

        /// <summary>
        /// 删除监视窗体
        /// </summary>
        /// <param name="MonitorCtrl">要监视的窗体</param>
        public void RemoveMonitorList(ICommandAction MonitorCtrl)
        {
            if (!MonitorCtrl.ReceiverList.Contains(this))
                MonitorCtrl.ReceiverList.Remove(this);
        }

        public void OnRecvMessage<T>(T msg)
        {
            var msgType = msg.GetType();
            string MethodName = "On" + msgType.Name;
            var method = GetType().GetMethod(MethodName);
            if (method != null)
            {
                method.Invoke(this, new object[] { msg });
            }
        }


        /// <summary>
        /// 用来更新PLC，Robbot，Scanner的状态
        /// </summary>
        /// <param name="msg"></param>
        public void OnMsgUpdateInstrumentState(MsgUpdateInstrumentState msg)
        {
            switch (msg.Instrument)
            {
                case EnumInsType.PLC:
                    StatePLC.Enabled=msg.IsEnable;
                    break;
                case EnumInsType.ROBOT:
                    StateRobot.Enabled = msg.IsEnable;
                    break;
                case EnumInsType.SCANNER:
                    StateScanner.Enabled = msg.IsEnable;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Manual和Auto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var PLC = InstrumentMgr<InstrumentCfgBase,CommunicationCfgBase>.Instance.FindInstanseByName("FX3UPLC") as InstrumentFxPLC;
            barCheckItem2.Caption = barCheckItem2.Checked ? "Manual" : "Auto";
            barCheckItem2.ImageIndex = barCheckItem2.Checked ? 1 : 2;
            if (PLC != null)
            {
                if (!PLC.IsOpen)
                    PLC.Open();
                if (PLC.IsOpen)
                {
                    PLC.WriteWord(REGISTER_TYPE.D,300,(short)(barCheckItem2.Checked ? 1000: 0));
                }
            }
        }

        public EnumSystemState SystemState
        {
            set {
                if (systemState != value)
                {
                    systemState = value;
                    UpdateButtonState();
                }
            }
            get
            {
                return systemState;
            }
        }

        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var station = StationMgr.Instance.FindInstanseByName("StationTest") as StationTest;
            if (station != null)
                station.PushStep(0);
        }

        private void UpdateButtonState()
        {
            switch (systemState)
            {
                case EnumSystemState.IDLE:
                    barButtonItemStart.Enabled = true;
                    barButtonItemStop.Enabled = false;
                    barButtonItemPuse.Enabled = true;
                    break;
                case EnumSystemState.PUSE:
                    barButtonItemStart.Enabled = true;
                    barButtonItemStop.Enabled = true;
                    barButtonItemPuse.Enabled = false;
                    break;
                case EnumSystemState.RUN:
                    barButtonItemStart.Enabled = false;
                    barButtonItemStop.Enabled = true;
                    barButtonItemPuse.Enabled = true;
                    break;
            }
        }

        private void barButtonItemPuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (StationMgr.Instance.PuseAllStation())
            {
                SystemState = EnumSystemState.PUSE;
            }
        }
    }
}
