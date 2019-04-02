using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdReadDoutBit : RobotCmdBase
    {
        public EnumDout I_Dout { get; set; }

        public bool Q_State { get; set; }
        public CmdReadDoutBit()
        {
            I_Cmd = EnumRobotCmd.READDOUT;
        }
        public override object GenEmptyCmd()
        {
            return new CmdReadDoutBit();
        }

        protected override void ReadProfile()
        {
            Q_State = Paras[0].Equals("1");
        }

        protected override void SetProfile()
        {
            Paras[0] = ((int)I_Dout).ToString();
        }
    }
}
