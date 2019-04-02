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

namespace Frame.View
{
    public partial class HistoryView : MessageUserControl
    {
        System.Collections.ObjectModel.ObservableCollection<Model.MessageModel> MsgCollect;
        public HistoryView()
        {
            InitializeComponent();
            MsgCollect=this.uC_HistoryPanel1.MsgCollect = new System.Collections.ObjectModel.ObservableCollection<Model.MessageModel>();
        }

        /// <summary>
        /// 将消息显示到消息框
        /// </summary>
        /// <param name="msg"></param>
        public void OnMsgOutput(MsgOutput msg)
        {
            MsgCollect.Add(msg.msg);
        }
    }
}
