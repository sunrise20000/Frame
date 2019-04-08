using Frame.Class.ViewCommunicationMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frame.Interface
{
    /// <summary>
    /// 发送消息能力
    /// </summary>
    public interface ICommandAction
    {
        void SendMessage<T>(T msg,ICommandAction Receive=null) where T : ViewMessageBase;
        void OnRecvMessage<T>(T msg);
        List<ICommandAction> ReceiverList { get; set; }
         
    }
}
