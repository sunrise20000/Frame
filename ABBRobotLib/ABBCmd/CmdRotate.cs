using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public class CmdRotate : RobotCmdBase
    {
        public CmdRotate()
        {
            I_Cmd = EnumRobotCmd.ROTATE;
            AngleX = AngleY = AngleZ = 0;
        }
        /// <summary>
        /// 绕X轴旋转的角度
        /// </summary>
        public double AngleX { get; set; }

        /// <summary>
        /// 绕Y轴旋转的角度
        /// </summary>
        public double AngleY { get; set; }

        /// <summary>
        /// 绕Z轴旋转的角度
        /// </summary>
        public double AngleZ { get; set; }

        /// <summary>
        /// 输入时填的参数
        /// </summary>
        /// <returns></returns>
        protected override void SetProfile()
        {
            Paras[0] = AngleX.ToString();
            Paras[1] = AngleY.ToString();
            Paras[2] = AngleZ.ToString();
        }

    
        public override object GenEmptyCmd()
        {
            return new CmdRotate();
        }

        /// <summary>
        /// 从结果中解析
        /// </summary>
        protected override void ReadProfile()
        {

        }
    }
}
