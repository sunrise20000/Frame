using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ABBRobotLib
{
    public class CmdInit : RobotCmdBase
    {
        public CmdInit()
        {
            I_Cmd = EnumRobotCmd.Init;
        }   

        protected override void SetProfile()
        {
            
        }
        public override object GenEmptyCmd()
        {
            return new CmdInit();
        }
        protected override void ReadProfile()
        {

        }
    }
}
