using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib
{
    public class CmdStopRobot : RobotCmdBase
    {
        public CmdStopRobot()
        {
            I_Cmd = EnumRobotCmd.StopRobot;
        }


        public override object GenEmptyCmd()
        {
            return new CmdStopRobot();
        }

        protected override void ReadProfile()
        {
            
        }

        protected override void SetProfile()
        {
            
        }
    }
}
