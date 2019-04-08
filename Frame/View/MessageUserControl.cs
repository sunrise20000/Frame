using Frame.Class.ViewCommunicationMessage;
using Frame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frame.View
{
    public class MessageUserControl : UserControl, ICommandAction
    {
        public List<ICommandAction> ReceiverList { get; set; }= new List<ICommandAction>();

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

        public void AddMonitorList(params ICommandAction[] MonitorCtrl)
        {
            foreach (var it in MonitorCtrl)
            {
                if (!it.ReceiverList.Contains(this))
                    it.ReceiverList.Add(this);
            }
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
            var method=GetType().GetMethod(MethodName);
            if (method != null)
            {
                method.Invoke(this,new object[] { msg});
            }
        }
    }
}
