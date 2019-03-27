using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdTest : RobotCmdBase
    {
        public CmdTest()
        {
            I_Cmd = EnumRobotCmd.NONE;
        }
        public override object GenEmptyCmd()
        {
            return new CmdTest();
        }

        protected override void SetProfile()
        {
            
        }
        protected override void ReadProfile()
        {

        }
    }
}
