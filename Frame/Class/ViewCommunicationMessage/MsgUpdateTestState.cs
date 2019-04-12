using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Class.ViewCommunicationMessage
{
    public enum EnumUpdateType
    {
        ScannerCode,
        Socket,
        FaceTest,
    }

    public enum EnumTestState
    {
        OK,
        NG,
        WAITING,
    }
    public class MsgUpdateTestState
    {
        public EnumUpdateType UpdateType { get; set; }
        public EnumTestState State { get; set; }

        public string StrMessage { get; set; }
    }
}
