using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdSetDoutBit : RobotCmdBase
    {
        public EnumDout I_Dout { get; set; }
        public bool I_State { get; set; }
        public CmdSetDoutBit()
        {
            I_Cmd = EnumRobotCmd.WRITEDOUT;
        }
        public override object GenEmptyCmd()
        {
            return new CmdSetDoutBit();
        }

        protected override void ReadProfile()
        {
            
        }

        protected override void SetProfile()
        {
            Paras[0] = ((int)I_Dout).ToString();
            Paras[1] = I_State ? "1" : "0";
        }
    }
}
