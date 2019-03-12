using Frame.Definations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Model
{
    public class MessageModel
    {
        public DateTime MsgTime { get; set; }
        public EnumMsgType MsgType { get; set; }
        public string MsgContent { get; set; }
        
        public MessageModel()
        {
            MsgTime = DateTime.Now;
            MsgType = EnumMsgType.Info;
            MsgContent = "";
        }
        public MessageModel(EnumMsgType type, string content)
        {
            MsgTime = DateTime.Now;
            MsgType = type;
            MsgContent = content;
        }
        public MessageModel(DateTime time,EnumMsgType type, string content)
        {
            MsgTime = time;
            MsgType = type;
            MsgContent = content;
        }
    }
}
