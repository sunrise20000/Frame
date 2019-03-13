﻿using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib
{
    public class MsgStopRobot : RobotCmdBase
    {
        public override object GenEmptyCmd()
        {
            return new MsgStopRobot() { I_Cmd = this.I_Cmd };
        }

        protected override void ReadProfile()
        {
            
        }

        protected override void SetProfile()
        {
            I_Cmd = EnumRobotCmd.StopRobot;
        }
    }
}
