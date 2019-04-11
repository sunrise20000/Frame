using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdMoveToPointReplaceXYZ : RobotCmdBase
    {
        public EnumProductType I_Type
        {
            get;set;
        }
        public double I_X { get; set; }
        public double I_Y { get; set; }
        public double I_Z { get; set; }

        public CmdMoveToPointReplaceXYZ()
        {
            I_Cmd = Definations.EnumRobotCmd.MOVETOPOINTREPLACEXYZ;
        }

        public override object GenEmptyCmd()
        {
            return new CmdMoveToPointReplaceXYZ();
        }

        protected override void ReadProfile()
        {
            ;
        }

        protected override void SetProfile()
        {
            Paras[0]=((int)I_Type).ToString();
            Paras[1] = I_X.ToString();
            Paras[2] = I_Y.ToString();
            Paras[3] = I_Z.ToString();
        }
    }
}
