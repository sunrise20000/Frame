using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Frame.Interface;
using Frame.Class.ViewCommunicationMessage;
using HalconDotNet;
using Frame.Instrument;
using Frame.Config.HardwareCfg.InstrumentCfg;
using Frame.Config.CommunicationCfg;

namespace Frame.View
{
    public partial class SettingView : MessageUserControl
    {
        InstrumentRobotABB Robot = null;
        public SettingView()
        {
            InitializeComponent();
        }

        public void OnMsgFindModel(MsgFindModel msg)
        {
            shapeModle1.LoadModle(@"Vision/Model/UpModel");
            var ret=shapeModle1.FindSimple();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shapeModle1.LoadModle(@"Vision/Model/UpModel");
            var ret = shapeModle1.FindSimple();
            HOperatorSet.ReadTuple(@"Vision/Calib/UP.tup", out HTuple hom2D);
            HOperatorSet.AffineTransPoint2d(hom2D,ret.Column, ret.Row, out HTuple Qx, out HTuple Qy);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Robot = Instrument.InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("RobotAbb") as Instrument.InstrumentRobotABB;
            if (!Robot.IsOpen)
            {
                Robot.Open();
            }

            shapeModle1.LoadModle(@"Vision/Model/DownModel");
            var ret = shapeModle1.FindSimple();
            
            HOperatorSet.ReadTuple(@"Vision/Calib/Down.tup", out HTuple hom2D);
            HOperatorSet.AffineTransPoint2d(hom2D, ret.Column, ret.Row, out HTuple Qx, out HTuple Qy);
            //304.44,-290.51
            double offsetX = Qx - 291.61847220;  //288.27739
            double offsetY = Qy + 290.95572;   //-292.0433

            //425.18, -152.73, 271.75

            double DesX = 425.18 - offsetX;
            double DesY = -152.73 - offsetY;

            Robot.MoveAbs(DesX, DesY, 271.75, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
            Robot.MoveAbs(DesX, DesY, 168.75, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
            Robot.MoveAbs(DesX, DesY, 271.75, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 10000);

        }

        private void buttonGetRotate_Click(object sender, EventArgs e)
        {
            Robot = Instrument.InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("RobotAbb") as Instrument.InstrumentRobotABB;
            if (!Robot.IsOpen)
            {
                Robot.Open();
            }
            shapeModle1.LoadModle(@"Vision/Model/DownModel");
            var ret = shapeModle1.FindSimple();
            HOperatorSet.ReadTuple(@"Vision/Calib/Down.tup", out HTuple hom2D);
            HOperatorSet.TupleDeg(ret.Angle, out HTuple angleOut);
            Robot.Rotate(0, 0, -angleOut, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Robot = Instrument.InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("RobotAbb") as Instrument.InstrumentRobotABB;
            if (!Robot.IsOpen)
            {
                Robot.Open();
            }
            Robot.GetPointPos(1, out double x, out double y, out double z);
        }
    }
}
