using Frame.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frame.Interface
{
    /// <summary>
    /// 赋予窗口发送消息的能力
    /// </summary>
    public interface ICommandAction
    {
        void SendMessage<T>(T msg,ICommandAction Receive=null) where T : ViewMessageBase;
        void OnRecvMessage<T>(T msg);
        List<ICommandAction> ReceiverList { get; set; }
         
    }
}
