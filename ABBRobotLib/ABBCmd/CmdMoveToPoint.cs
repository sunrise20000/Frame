using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdMoveToPoint : RobotCmdBase
    {
        public int I_PointID { get; set; } = -1;
        public CmdMoveToPoint()
        {
            I_Cmd = Definations.EnumRobotCmd.MOVETOPOINT;
        }

        public override object GenEmptyCmd()
        {
            return new CmdMoveToPoint();
        }

        protected override void ReadProfile()
        {
            
        }

        protected override void SetProfile()
        {
            Paras[0]=I_PointID.ToString();           
        }
    }
}
