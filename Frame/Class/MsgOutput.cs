using Frame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Class
{
    /// <summary>
    /// 用来显示运行过程中的信息
    /// </summary>
    public class MsgOutput : ViewMessageBase
    {
        public MessageModel msg { get; set; } = new MessageModel();   
    }
}
