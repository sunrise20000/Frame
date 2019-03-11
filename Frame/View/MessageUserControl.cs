using Frame.Class;
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
        public List<MessageUserControl> ReceiverList { get; private set; } = new List<MessageUserControl>();

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
        /// <param name="Receive"></param>
        public void AddMonitorList(MessageUserControl MonitorObj)
        {
            if (!MonitorObj.ReceiverList.Contains(this))
                MonitorObj.ReceiverList.Add(this);
        }

        /// <summary>
        /// 删除监视窗体
        /// </summary>
        /// <param name="Receive"></param>
        public void RemoveMonitorList(MessageUserControl MonitorObj)
        {
            if (!MonitorObj.ReceiverList.Contains(this))
                MonitorObj.ReceiverList.Remove(this);
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
