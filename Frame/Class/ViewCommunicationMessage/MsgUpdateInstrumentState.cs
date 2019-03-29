using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Class.ViewCommunicationMessage
{
    public enum EnumInsType
    {
        PLC,
        ROBOT,
        SCANNER,
    }
    public class MsgUpdateInstrumentState
    {
        public EnumInsType Instrument { get; set; }
        public bool IsEnable { get; set; }
    }

}
