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
using Frame.Class.ViewCommunicationMessage;
using Frame.Instrument;
using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;

namespace Frame.View
{
    public partial class HistoryView : MessageUserControl
    {
        System.Collections.ObjectModel.ObservableCollection<Model.MessageModel> MsgCollect;
        public HistoryView()
        {
            InitializeComponent();
            MsgCollect=this.uC_HistoryPanel1.MsgCollect = new System.Collections.ObjectModel.ObservableCollection<Model.MessageModel>();
            uC_HistoryPanel1.OnClearClicked += UC_HistoryPanel1_OnClearClicked;
        }

        private void UC_HistoryPanel1_OnClearClicked(object sender, EventArgs e)
        {
            var PLC = InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("FX3UPLC") as InstrumentFxPLC;
            if (PLC != null)
            {
                if (!PLC.IsOpen)
                {
                    PLC.Open();

                }
                if(PLC.IsOpen)
                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,301,1000);
            }
            
        }

        public void OnMsgOutput(MsgOutput msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnMsgOutput(msg)));
            }
            else
                MsgCollect.Add(msg.msg);
        }
    }
}
