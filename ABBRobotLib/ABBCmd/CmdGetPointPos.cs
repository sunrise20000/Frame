using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdGetPointPos : RobotCmdBase
    {
        public int I_PointID { get; set; }
        public double Q_X { get; private set; }
        public double Q_Y { get; private set; }
        public double Q_Z { get; private set; }


        public CmdGetPointPos()
        {
            I_Cmd = Definations.EnumRobotCmd.GETPOINTPOS;
        }

        /// <summary>
        /// 发送给机械手的
        /// </summary>
        protected override void SetProfile()
        {
            Paras[0] = I_PointID.ToString();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 机械手返回的东西
        /// </summary>
        protected override void ReadProfile()
        {
            //throw new NotImplementedException();
            double.TryParse(Paras[0], out double x);
            double.TryParse(Paras[1], out double y);
            double.TryParse(Paras[2], out double z);
            Q_X = x;
            Q_Y = y;
            Q_Z = z;

        }

        public override object GenEmptyCmd()
        {
            return new CmdGetPointPos();
        }

     
    }
}
