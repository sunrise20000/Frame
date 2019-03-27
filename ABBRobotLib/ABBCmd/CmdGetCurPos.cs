using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdGetCurPos : RobotCmdBase
    {
        public double Q_X { get; set; }
        public double Q_Y { get; set; }
        public double Q_Z { get; set; }
        public CmdGetCurPos()
        {
            I_Cmd = EnumRobotCmd.GETCURPOSXYZ;
        }
        public override object GenEmptyCmd()
        {
            return new CmdGetCurPos();
        }

        protected override void ReadProfile()
        {
            bool bRet = true;
            bRet &= double.TryParse(Paras[0], out double x);
            bRet &= double.TryParse(Paras[1], out double y);
            bRet &= double.TryParse(Paras[2], out double z);
            bRet &= Paras[5].ToUpper().Equals("OK");
            if (!bRet)
                throw new Exception("Wrong robot coordinate when parse calibdata");
            this.Q_X = x;
            this.Q_Y = y;
            this.Q_Z = z;
        }

        protected override void SetProfile()
        {
            
        }
    }
}
