using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib
{
    public class CmdMoveToPos : RobotCmdBase
    {
        public CmdMoveToPos()
        {
            I_Cmd = EnumRobotCmd.MoveToPos;
        }

        public double I_X { get; set; }
        public double I_Y { get; set; }
        public double I_Z { get; set; }

        public EnumMoveType MoveType { get; set; }

        protected override void SetProfile()
        {
            Paras[0] = I_X.ToString();
            Paras[1] = I_Y.ToString();
            Paras[2] = I_Z.ToString();
            Paras[3] = ((int)MoveType).ToString();

        }
        public override object GenEmptyCmd()
        {
            return new CmdMoveToPos();
        }
        protected override void ReadProfile()
        {
            
        }
    }
}
