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
        public List<ICommandAction> ListenerList { get; set; }= new List<ICommandAction>();

        public void SendMessage<T>(T msg, ICommandAction Listener = null) where T : ViewMessageBase
        {
            if (Listener == null)
            {
                foreach (var it in ListenerList)
                    it.OnRecvMessage(msg);
            }
            else
            {
                Listener.OnRecvMessage(msg);
            }
        }

        /// <summary>
        /// 添加监视窗体
        /// </summary>
        /// <param name="MonitorCtrl">要监视的窗体</param>
        public void AddListener(ICommandAction ListernerCtrl)
        {
            if (ListenerList.Contains(ListernerCtrl))
                ListenerList.Add(ListernerCtrl);
        }

        public void AddListener(params ICommandAction[] ListernerCtrl)
        {
            foreach (var it in ListernerCtrl)
            {
                if (!ListenerList.Contains(it))
                    ListenerList.Add(it);
            }
        }
        /// <summary>
        /// 删除监视窗体
        /// </summary>
        /// <param name="MonitorCtrl">要监视的窗体</param>
        public void RemoveListenerList(ICommandAction ListernerCtrl)
        {
            if (!ListenerList.Contains(ListernerCtrl))
                ListenerList.Remove(ListernerCtrl);
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
