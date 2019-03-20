using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXPLCCommunicationLib
{
    /// <summary>
    /// 通信只支持D就可以了
    /// </summary>
    public enum REGISTER_TYPE
    {
      D,
    }
    public enum CMD
    {
        STX = 0x02,
        R = 0x30,
        W = 0x31,
        ETX = 0x03,
        ACK = 0x06,
        NCK = 0x15,
    };
}
