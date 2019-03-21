using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HalWindow;
using Frame.Model;
using ViewROI;
using HalconDotNet;
using Frame.Class;

namespace Frame.View
{
    public partial class HomeView : MessageUserControl
    {
        RapidSettingModel rapidSetting = new RapidSettingModel();
        DataTable _dt = new DataTable();

        public HomeView()
        {
            InitializeComponent();
            InitCtrl();
        }

        private void InitCtrl()
        {
            //
            propertyGridControl1.SelectedObject = rapidSetting;


            //初始化DataGrid
            _dt.Columns.Add("Time", Type.GetType("System.DateTime"));
            _dt.Columns.Add("Type", Type.GetType("System.String"));
            _dt.Columns.Add("Content", Type.GetType("System.String"));
            dataGridView1.DataSource = _dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 15;
            dataGridView1.Columns[2].FillWeight = 65;
        }

        public void OnMsgOutput(MsgOutput msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(()=> OnMsgOutput(msg)));
            }
            else
            {
                var newdr = _dt.NewRow();
                newdr[0] = msg.msg.MsgTime;
                newdr[1] = msg.msg.MsgType;
                newdr[2] = msg.msg.MsgContent;
                _dt.Rows.Add(newdr);
                while (_dt.Rows.Count > 5)
                {
                    _dt.Rows.RemoveAt(0);
                }

            }
        }

        //消息处理
        public void OnMsgStationInfo(MsgStationInfo msg)
        {
         
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(()=>OnMsgStationInfo(msg)));
            }
            else
            {
                var station = StationMgr.Instance.FindInstanseByName(msg.SenderName);
                if (station != null)
                    uC_StationInfo1.ShowInfo(msg.Infomation, station);
            }
        }

        public void SetStationBinding(params StationBase[] stations)
        {
            uC_StationInfo1.SetStationList(stations);
        }

        //Debug
        private int i=0;
        private void HomeView_MouseClick(object sender, MouseEventArgs e)
        {
            var msg = new MsgOutput()
            {
                msg = new MessageModel(Definations.EnumMsgType.Error, i++.ToString())
            };
            SendMessage(msg);
            OnMsgOutput(msg);
        }
    }
}
