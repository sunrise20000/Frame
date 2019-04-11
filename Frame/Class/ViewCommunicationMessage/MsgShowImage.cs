using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
namespace Frame.Class.ViewCommunicationMessage
{
    public class MsgShowImage : ViewMessageBase
    {
        public HObject Image { get; set; }
        public string mess { get; set; }
    }
}
