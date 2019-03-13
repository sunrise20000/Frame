using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib
{
    public class MsgTest : RobotCmdBase
    {
        public override object GenEmptyCmd()
        {
            return new MsgTest() { I_Cmd = this.I_Cmd };
        }

        protected override void SetProfile()
        {
            I_Cmd = EnumRobotCmd.Test;
        }
        protected override void ReadProfile()
        {

        }
    }
}
