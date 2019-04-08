using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data;
using System.IO;
using Frame.Definations;
using Frame.Instrument;
using Frame.Config.HardwareCfg.InstrumentCfg;
using Frame.Config.CommunicationCfg;
using Frame.Class.ViewCommunicationMessage;
using CLCamera;
using Frame.Camera;
using HalconDotNet;
namespace Frame.Model
{
   
    class StationTest : StationBase
    {
        const string PLCNAME = "FX3UPLC";
        const string ROBOTNAME = "RobotAbb";
        const string SCANNERNAME = "Scanner";

        InstrumentFxPLC PLC = InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName(PLCNAME) as InstrumentFxPLC;
        InstrumentRobotABB Robot = InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName(ROBOTNAME) as InstrumentRobotABB;
        InstrumentScanner Scanner = InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName(SCANNERNAME) as InstrumentScanner;
        HalconModle.ShapeModle shapeModleUp = new HalconModle.ShapeModle();

        HalconModle.ShapeModle shapeModleDown = new HalconModle.ShapeModle();
        Frame.Camera.HaiKangCamera upCam;
        Frame.Camera.HaiKangCamera downCam;
        Dictionary<int, STEP1> MappingDic = new Dictionary<int, STEP1>();
        HObject m_image;
        double m_commonZ;//所有上行点位公用Z轴高度
        ABBRobotLib.Definations.EnumRobotSpeed CommonSpeed;
        STEP1 nStep;
        public StationTest()
        {
            CommonSpeed = ABBRobotLib.Definations.EnumRobotSpeed.V100;
            Robot.Open();

            upCam = Camera.CameraManager.Instance.FindInstanseByName("Cam_Up") as Frame.Camera.HaiKangCamera;
            if (!upCam.m_IsConnected)
            {
                upCam.OpenCamera();
                upCam.SetTriggerMode("Off");
                upCam.ImageAcquired -= UpCam_ImageAcquired;
                upCam.ImageAcquired += UpCam_ImageAcquired;
            }


            downCam = Camera.CameraManager.Instance.FindInstanseByName("Cam_Down") as Frame.Camera.HaiKangCamera;
            if (!downCam.m_IsConnected)
            {
                downCam.OpenCamera();
                downCam.SetTriggerMode("Off");
                downCam.ImageAcquired -= DownCam_ImageAcquired;
                downCam.ImageAcquired += DownCam_ImageAcquired;
            }
            PLC.Open();

            MappingDic.Add(0, STEP1.GrapProduct);
            MappingDic.Add(1, STEP1.SocketTest);
            MappingDic.Add(2, STEP1.PlayToFaceTest);
            MappingDic.Add(3, STEP1.FaceTest);
            MappingDic.Add(4, STEP1.GetDustPlug);
            MappingDic.Add(5, STEP1.UnloadProduct);
        }

       

        ~StationTest()
        {
            Robot.Close();
        }
        protected override bool UserInit()
        {
            try
            {
                var list=PLC.ReadBlockInt("D100", 10);
            }
            catch(Exception ex)
            {
                ShowInfo(ex.Message);
            }
            //Console.WriteLine("Getit");

            //var ret = Robot.GetCurrentPostion(ABBRobotLib.Definations.EnumRobotTool.Tool0);
            //ShowInfo($"{ret}");
            //ShowInfo("读取条码：12345678");
            //SendMessage(new MsgOutput() {msg=new MessageModel(EnumMsgType.Info,"12345678") });
            return true;
        }
        protected override int WorkFlow()
        { 
            double x, y, z;
            ClearAllStep();
            PushStep(STEP1.Init);
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    Dispatcher();
                    if (CheckStationStatusChanged() || nStepQueue.Count == 0)
                        continue;
                    nStep = PeekStep<STEP1>();
                    switch (nStep)
                    {
                        case STEP1.Init://复位机械手
                            ShowInfo("Init");
                            Robot.GetPointPos((int)MachinePoint.GrapProductUp, out  x, out  y, out m_commonZ);
                            var WordPt = Robot.GetCurrentPostion(ABBRobotLib.Definations.EnumRobotTool.Tool1);
                            Robot.MoveAbs(WordPt.X, WordPt.Y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 30000);
                            Robot.MoveToPoint((int)MachinePoint.InitPoint, CommonSpeed, 20000);
                            shapeModleUp.LoadModle(@"Vision/Model/UpModel");
                            shapeModleDown.LoadModle(@"Vision/Model/DownModel");

                            bool ret = Robot.SetDoutBit(0, false);
                            if (!ret)
                            {
                                PLC.WriteWord("D100", 2001);
                                PopStep();
                                throw new Exception("关闭吸真空失败");

                            }
                            ret = Robot.SetDoutBit(ABBRobotLib.Definations.EnumDout.Dout2, false);
                            if (!ret)
                            {
                                PLC.WriteWord("D100", 2001);
                                PopStep();
                                throw new Exception("打开夹爪失败");

                            }

                            PLC.WriteWord("D98", 1000);
                            ClearAllStep();
                            PopAndPushStep(STEP1.WaitInitOk);
                            break;
                        case STEP1.WaitInitOk:
                            ShowInfo("WaitInitOk");
                            if (PLC.ReadWord("D98") == 0)
                            {
                                ClearAllStep();
                                PLC.ForceMBit("M2", true);
                            }
                            Thread.Sleep(1000);
                            break;
                        case STEP1.GrapProduct:
                            ShowInfo("GrapProduct");
                            upCam.SnapShot();
                            Thread.Sleep(500);
                            shapeModleUp.BackImage = m_image;
                            var result = shapeModleUp.FindSimple();
                            if (result != null)
                            {
                                HOperatorSet.ReadTuple(@"Vision/Calib/Up.tup", out HTuple hom2D);
                                HOperatorSet.AffineTransPoint2d(hom2D, result.Column, result.Row, out HTuple Qx, out HTuple Qy);
                                Robot.MoveToPoint((int)MachinePoint.GrapProductUp, CommonSpeed, 20000);
                                Robot.GetPointPos((int)MachinePoint.GrapProductDown, out x, out y, out z);
                                Robot.MoveAbs(Qx, Qy, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                                ret = Robot.SetDoutBit(0, true);
                                if (!ret)
                                {
                                    PLC.WriteWord("D100", 2001);
                                    PopStep();
                                    throw new Exception("打开吸真空失败");

                                }
                                Robot.MoveAbs(Qx, Qy, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                                Robot.MoveToPoint((int)MachinePoint.SnapPosition, CommonSpeed, 20000);
                                ClearAllStep();
                                PushStep(STEP1.PlayToSocket);
                            }                          
                           else
                                ClearAllStep();
                            break;
                        case STEP1.PlayToSocket:

                            downCam.SnapShot();
                            Thread.Sleep(500);
                            shapeModleDown.BackImage = m_image;
                            result = shapeModleDown.FindSimple();
                            if (result != null)
                            {
                                HOperatorSet.TupleDeg(result.Angle, out HTuple angleOut);
                                Robot.Rotate(0, 0, -angleOut, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1);
                            }
                            else
                            {
                                ShowInfo("未能抓取到产品1");
                                PopStep();
                                PLC.WriteDword("D101", 2001);
                                break;
                            }

                            downCam.SnapShot();
                            Thread.Sleep(500);
                            shapeModleDown.BackImage = m_image;
                            result = shapeModleDown.FindSimple();
                            if (result != null)
                            {
                                HOperatorSet.ReadTuple(@"Vision/Calib/Down.tup", out HTuple hom2D);
                                HOperatorSet.AffineTransPoint2d(hom2D, result.Column, result.Row, out HTuple Qx, out HTuple Qy);

                                double offsetX = Qx - 291.61847220;  //288.27739
                                double offsetY = Qy + 290.95572;   //-292.0433

                                double DesX = 425.18 - offsetX;
                                double DesY = -152.73 - offsetY;

                                Robot.MoveAbs(DesX, DesY, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                                Robot.MoveAbs(DesX, DesY, 168.75, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                                Robot.GetPointPos((int)MachinePoint.SocketDown, out x, out y, out z);
                                Robot.MoveAbs(DesX, DesY, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 10000);


                                ret = Robot.SetDoutBit(0, false);
                                if (!ret)
                                {
                                    PLC.WriteWord("D100", 2001);
                                    throw new Exception("关闭吸真空失败");
                                }
                                Robot.MoveAbs(DesX, DesY, m_commonZ, ABBRobotLib.Definations.EnumRobotSpeed.V100, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 10000);
                                PLC.WriteDword("D100", 1001);
                                ClearAllStep();
                                PushStep(STEP1.SocketTest);
                            }
                            else
                            {
                                ShowInfo("未能抓取到产品2");
                                PopStep();
                                PLC.WriteWord("D100", 2001);
                                break;
                            }

                            break;

                        case STEP1.SocketTest:
                            ShowInfo("SocketTest");
                            Thread.Sleep(3000);
                            PLC.WriteWord("D101", 1001);
                            PopStep();
                            break;
                        case STEP1.PlayToFaceTest:
                            ShowInfo("PlayToFaceTest");
                            Robot.MoveToPoint((int)MachinePoint.SocketDown, CommonSpeed, 20000);
                            ret = Robot.SetDoutBit(0, true);
                            if (!ret)
                            {
                                throw new Exception("打开吸真空失败");
                            }
                            Robot.GetPointPos((int)MachinePoint.SocketDown, out x, out y, out z);
                            Robot.MoveAbs(x, y, m_commonZ, ABBRobotLib.Definations.EnumRobotSpeed.V100, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.GetPointPos((int)MachinePoint.EndFaceDown, out x, out y, out z);
                            Robot.MoveAbs(x, y, m_commonZ, ABBRobotLib.Definations.EnumRobotSpeed.V100, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.MoveAbs(x, y, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            ret = Robot.SetDoutBit(0, false);
                            if (!ret)
                            {
                                throw new Exception("关闭吸真空失败");
                            }
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            PLC.WriteWord("D102", 1001);
                            PopStep();
                            break;
                        case STEP1.FaceTest:
                            ShowInfo("FaceTest");
                            Thread.Sleep(3000);
                            PLC.WriteWord("D103", 1001);
                            PopStep();
                            break;
                        case STEP1.GetDustPlug:

                            Robot.GetPointPos((int)MachinePoint.GrapDustPlugBack, out x, out y, out z);
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 30000);
                            Robot.MoveToPoint((int)MachinePoint.GrapDustPlugBack, CommonSpeed, 10000);//主要为了将夹爪旋转到位
                            Robot.MoveToPoint((int)MachinePoint.GrapDustPlugFront, CommonSpeed, 10000);
                            ret = Robot.SetDoutBit(ABBRobotLib.Definations.EnumDout.Dout2, true);
                            if (!ret)
                            {
                                throw new Exception("机械臂夹爪夹紧失败");
                            }
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.GetPointPos((int)MachinePoint.EquipDustPlugBack, out x, out y, out z);
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.MoveAbs(x, y, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.MoveToPoint((int)MachinePoint.EquipDustPlugFront, CommonSpeed, 20000);
                            ret = Robot.SetDoutBit(ABBRobotLib.Definations.EnumDout.Dout2, false);
                            if (!ret)
                            {
                                throw new Exception("机械臂夹爪松开失败");
                            }
                            Robot.MoveAbs(x, y, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            PLC.WriteWord("D104", 1001);
                            PopStep();
                            break;
                        case STEP1.UnloadProduct:
                            Robot.GetPointPos((int)MachinePoint.UnloadProduct, out x, out y, out z, 1000);
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 90000);
                            Robot.MoveAbs(x, y, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 40000);
                            ret = Robot.SetDoutBit(ABBRobotLib.Definations.EnumDout.Dout1, true);
                            if (!ret)
                            {
                                throw new Exception("开吸真空失败");
                            }
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);

                            Robot.GetPointPos((int)MachinePoint.NgProduct2, out x, out y, out z);
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            Robot.MoveAbs(x, y, z, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            ret = Robot.SetDoutBit(ABBRobotLib.Definations.EnumDout.Dout1, false);
                            if (!ret)
                            {
                                throw new Exception("开吸真空失败");
                            }
                            Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveL, 20000);
                            PLC.WriteWord("D105", 1001);

                            PopStep();
                            break;

                        case STEP1.REST:
                            break;


                        default:
                            ShowInfo("Default");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ShowInfo($"程序中止:{ex.Message}");
                }
            }


            return 0;
        }


        private void UpCam_ImageAcquired(object sender, ImageEventArgs<HObject> e)
        {
            m_image = e.image;
            //HOperatorSet.WriteImage(e.image, "bmp", 0, "123.bmp");
            SendMessage(new MsgShowImage()
            {
                Image = e.image,
            });
        }
        private void DownCam_ImageAcquired(object sender, ImageEventArgs<HalconDotNet.HObject> e)
        {
            m_image = e.image;
            SendMessage(new MsgShowImage()
            {
                Image = e.image,  
            });
        }

        private void SnapAndPick()
        {

        }

        private void SocketTest()
        {

        }

        private void PickAfterSocket()
        {

        }
        enum MachinePoint
        {
            GrapProductUp=1,
            GrapProductDown,
            SnapPosition,
            ReadCode,
            SocketDown,
            EndFaceDown,
            GrapDustPlugFront,
            GrapDustPlugBack,
            EquipDustPlugFront,
            EquipDustPlugBack,
            NgProduct1,
            NgProduct2,
            UnloadProduct,
            InitPoint,
        }

        private void Dispatcher()
        {
            var IntList = PLC.ReadBlockInt("D100", 10);
            for(int i= MappingDic.Count-1; i>-1;i--)
            {
                if (IntList[i] == 1&& !nStepQueue.Contains((int)MappingDic[i]))
                {
                    PushStep(MappingDic[i]);
                    ShowInfo($"当前压入步骤{MappingDic[i].ToString()}");
                    break;
                }
            }
        }
    }
}
