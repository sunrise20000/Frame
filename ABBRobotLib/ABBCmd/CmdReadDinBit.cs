using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdReadDinBit : RobotCmdBase
    {
        public EnumDin I_Din { get; set; }

        public bool Q_State { get; private set; }
        public CmdReadDinBit()
        {
            I_Cmd = EnumRobotCmd.READDIN;
        }


        public override object GenEmptyCmd()
        {
            return new CmdReadDinBit();
        }

        protected override void ReadProfile()
        {
            Q_State=Paras[0].Equals("1");
        }

        protected override void SetProfile()
        {
            Paras[0]=((int)I_Din).ToString();
        }
    }
}
