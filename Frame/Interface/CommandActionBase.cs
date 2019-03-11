using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Frame.Class;

namespace Frame.Interface
{
    public abstract class CommandActionBase : UserControl , ICommandAction 
    {
        private List<CommandActionBase> ReceiverList = new List<CommandActionBase>();

        public void SendMessage<T>(T msg) where T:ViewMessageBase
        {
            foreach (var it in ReceiverList)
                OnRecvMessage(msg);
        }


        /// <summary>
        /// 添加监视窗体
        /// </summary>
        /// <param name="Receive"></param>
        protected void AddMonitorList(CommandActionBase Receive)
        {
            if (!ReceiverList.Contains(Receive))
                ReceiverList.Add(Receive);
        }

        /// <summary>
        /// 删除监视窗体
        /// </summary>
        /// <param name="Receive"></param>
        protected void RemoveMonitorList(CommandActionBase Receive)
        {
            if (!ReceiverList.Contains(Receive))
                ReceiverList.Remove(Receive);
        }

        protected abstract void OnRecvMessage<T>(T msg);
    }
}
