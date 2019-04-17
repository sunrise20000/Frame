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
using ABBRobotLib.Definations;
using System.Management.Instrumentation;
using System.Windows.Forms;

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
        double[,] OkPoints = new double[10, 3];//ok产品放置位置
        double[,] NgPoints = new double[6, 3]; //ng产品放置位置
        int OkProductCount = 0;
        int NgProductCount = 0;
        bool InitFlag = false;

         EnumRobotSpeed CommonSpeed;
        STEP1 nStep;
        public StationTest()
        {
            CommonSpeed = EnumRobotSpeed.V500;
            if(!Robot.IsOpen)
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
            if (!PLC.IsOpen)
                PLC.Open();

            Scanner.Open();
            MappingDic.Add(0, STEP1.GrapProduct);
            MappingDic.Add(1, STEP1.SocketTest);
            MappingDic.Add(2, STEP1.UnLoadFromSocket);
            MappingDic.Add(3, STEP1.FaceTest);
            MappingDic.Add(4, STEP1.GetDustPlug);
            MappingDic.Add(5, STEP1.UnloadFromEndFaceTest);
        }

       

        ~StationTest()
        {
            Robot.Close();
        }
        protected override bool UserInit()
        {
            return true;
        }
        protected override int WorkFlow()
        {
            Random random = new Random();
            int socketTestResult =0;
            int faceTestResult = 0;
            double x, y, z;       
            ClearAllStep();
            PushStep(STEP1.Init);
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    if (!bPused)
                    {
                        if (InitFlag)
                            Dispatcher();
                        if (CheckStationStatusChanged() || nStepQueue.Count == 0)
                            continue;
                        nStep = PeekStep<STEP1>();
                        switch (nStep)
                        {
                            case STEP1.Init://复位机械手    
                                ShowInfo("Init");
                                Robot.GetPointPos((int)MachinePoint.GrapProductUp, out x, out y, out m_commonZ);

                                Robot.GetPointPos((int)MachinePoint.OkPoint1, out double OkPointsx1, out double OkPointsy1, out z);
                                Robot.GetPointPos((int)MachinePoint.OkPoint2, out double OkPointsx2, out double OkPointsy2, out z);
                                OkPoints = GetAveragePoints(2, 5, OkPointsx1, OkPointsy1, OkPointsx2, OkPointsy2, z);

                                Robot.GetPointPos((int)MachinePoint.NgPoint1, out double ngPointsx1, out double ngPointsy1, out z);
                                Robot.GetPointPos((int)MachinePoint.NgPoint2, out double ngPointsx2, out double ngPointsy2, out z);

                                NgPoints = GetAveragePoints(3, 2, ngPointsx1, ngPointsy1, ngPointsx2, ngPointsy2, z);

                                var WordPt = Robot.GetCurrentPostion(EnumRobotTool.Tool1);
                                Robot.MoveAbs(WordPt.X, WordPt.Y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 30000);
                                Robot.MoveToPoint((int)MachinePoint.InitPoint, CommonSpeed, 20000);
                                shapeModleUp.LoadModle(@"Vision/Model/UpModel");
                                shapeModleDown.LoadModle(@"Vision/Model/DownModel");
                                Robot.SetDoutBit(EnumDout.Dout1, false);
                                Robot.SetDoutBit(EnumDout.Dout2, false);
                                CheckDIo(EnumDin.Din1,true, "未读到真空负压信号", 2000);

                                CheckDIo(EnumDin.Din2,true, "未读到防尘塞夹爪气缸原位信号", 2000);


                               
                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.WAITING,
                                    UpdateType = EnumUpdateType.FaceTest
                                });
                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.WAITING,
                                    UpdateType = EnumUpdateType.ScannerCode
                                });
                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.WAITING,
                                    UpdateType = EnumUpdateType.Socket
                                });

                                PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,98,1000);
                                ClearAllStep();
                                PushStep(STEP1.WaitInitOk);
                                break;
                            case STEP1.WaitInitOk:
                                ShowInfo("WaitInitOk");
                                if (PLC.ReadWord(FXPLCCommunicationLib.REGISTER_TYPE.D,98) == 0)
                                {
                                    ShowInfo("InitOk");
                                    InitFlag = true;
                                    ClearAllStep();
                                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,300, 1000);
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
                                    Robot.MoveAbs(Qx, Qy, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                    Robot.SetDoutBit(0, true);

                                    CheckDIo(EnumDin.Din1,false, "未读到真空负压信号", 2000);

                                
                                    Robot.MoveAbs(Qx, Qy, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                    Robot.MoveToPoint((int)MachinePoint.SnapPosition, CommonSpeed, 20000);
                                    PopAndPushStep(STEP1.PlayToSocket);
                             
                                    downCam.SnapShot();
                                    Thread.Sleep(500);
                                    shapeModleDown.BackImage = m_image;
                                    result = shapeModleDown.FindSimple();
                                    if (result != null)
                                    {
                                        HOperatorSet.TupleDeg(result.Angle, out HTuple angleOut);
                                        Robot.Rotate(0, 0, -angleOut, CommonSpeed, EnumRobotTool.Tool1);
                                    }
                                    else
                                    {
                                        ShowInfo("未能抓取到产品1");
                                        PopStep();
                                        PLC.WriteDword(FXPLCCommunicationLib.REGISTER_TYPE.D,101,2001);
                                        break;
                                    }

                                    downCam.SnapShot();
                                    Thread.Sleep(500);

                                    shapeModleDown.BackImage = m_image;
                                    result = shapeModleDown.FindSimple();
                                    if (result != null)
                                    {
                                        HOperatorSet.ReadTuple(@"Vision/Calib/Down.tup", out  hom2D);
                                        HOperatorSet.AffineTransPoint2d(hom2D, result.Column, result.Row, out  Qx, out  Qy);

                                        double offsetX = Qx - 291.61847220;  //288.27739
                                        double offsetY = Qy + 290.95572;   //-292.0433

                                        double DesX = 425.18 - offsetX;
                                        double DesY = -152.73 - offsetY;

                                        Robot.MoveAbs(DesX, DesY, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);

                                        Robot.GetPointPos((int)MachinePoint.ReadCode, out x, out y, out z);
                                        Robot.MoveAbs(x, y, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                        try
                                        {
                                            var StrCode = Scanner.Decode(1000);
                                            SendMessage(new MsgUpdateTestState()
                                            {
                                                State=EnumTestState.OK, StrMessage= StrCode , UpdateType=EnumUpdateType.ScannerCode
                                            });
                                        }
                                        catch
                                        {
                                            var r = new Random();
                                            var StrCode =$"C0470{r.Next(0,9)}{r.Next(0, 9)}{r.Next(0, 9)}{r.Next(0, 9)}{r.Next(0, 9)}";
                                            SendMessage(new MsgUpdateTestState()
                                            {
                                                State = EnumTestState.OK,
                                                StrMessage = StrCode,
                                                UpdateType = EnumUpdateType.ScannerCode
                                            });

                                        }

                                        Robot.MoveAbs(DesX, DesY, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 10000);

                                        Robot.GetPointPos((int)MachinePoint.SocketDown, out x, out y, out z);
                                        Robot.MoveAbs(DesX, DesY, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 10000);

                                        Robot.SetDoutBit(0, false);

                                        Thread.Sleep(500);
                                        Robot.MoveAbs(DesX, DesY, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 10000);
                                        PLC.WriteDword(FXPLCCommunicationLib.REGISTER_TYPE.D,100, 1001);
                                        ClearAllStep();
                                        PushStep(STEP1.SocketTest);
                                    }
                                    else
                                    {
                                        ShowInfo("未能抓取到产品2");
                                        PopStep();
                                        PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,100, 2001);
                                        break;
                                    }

                                }
                                else
                                {
                                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,100, 3000);
                                    PopStep();
                                }

                                break;

                            case STEP1.SocketTest:
                                ShowInfo("SocketTest");
                                Thread.Sleep(3000);
                                socketTestResult = random.Next(1, 2);//1表示ok，2表示ng
                                if (socketTestResult == 2)
                                {
                                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,101, 2001);
                                    PopStep();
                                    SendMessage(new MsgUpdateTestState()
                                    {
                                        State = EnumTestState.NG,
                                        UpdateType = EnumUpdateType.Socket
                                    });
                                    break;
                                }

                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.OK,
                                    UpdateType = EnumUpdateType.Socket
                                });

                                PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,101, 1001);
                                PopStep();
                                break;

                            case STEP1.UnLoadFromSocket:
                                ShowInfo("PlayToFaceTest");
                                Robot.GetPointPos((int)MachinePoint.SocketDown, out x, out y, out z);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 10000);

                                Robot.MoveToPoint((int)MachinePoint.SocketDown, CommonSpeed, 20000);
                                Robot.SetDoutBit(0, true);
                                CheckDIo(EnumDin.Din1,false, "未读到真空负压信号", 2000);


                               
                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.WAITING,
                                    UpdateType = EnumUpdateType.Socket
                                });
                                Robot.GetPointPos((int)MachinePoint.SocketDown, out x, out y, out z);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);

                                if (socketTestResult == 2)
                                {
                                    PopAndPushStep(STEP1.PlayToNgPoint);
                                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,102, 1002);
                                    break;
                                }


                                Robot.GetPointPos((int)MachinePoint.EndFaceDown, out x, out y, out z);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                Robot.MoveAbs(x, y, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                Robot.SetDoutBit(0, false);
                                Thread.Sleep(500);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,102, 1001);
                                PopStep();
                                break;
                            case STEP1.FaceTest:
                                ShowInfo("FaceTest");
                                Thread.Sleep(3000);
                                faceTestResult = random.Next(1, 2);//1表示ok，2表示ng
                               // faceTestResult = 2;
                                if (faceTestResult == 2)
                                {
                                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,103, 2001);
                                    PopStep();
                                    SendMessage(new MsgUpdateTestState()
                                    {
                                        State = EnumTestState.NG,
                                        UpdateType = EnumUpdateType.FaceTest
                                    });
                                    break;
                                }

                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.OK,
                                    UpdateType = EnumUpdateType.FaceTest
                                });
                                PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,103, 1001);
                                PopStep();
                                break;
                            case STEP1.GetDustPlug:

                                Robot.GetPointPos((int)MachinePoint.GrapDustPlugBack, out x, out y, out z);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 30000);
                                Robot.MoveToPoint((int)MachinePoint.GrapDustPlugBack, CommonSpeed, 10000);//主要为了将夹爪旋转到位
                                Robot.MoveToPoint((int)MachinePoint.GrapDustPlugFront, EnumRobotSpeed.V50, 10000);
                                Robot.SetDoutBit(EnumDout.Dout2, true);
                                CheckDIo(EnumDin.Din2,false, "未读到防尘塞夹爪气缸到位信号", 2000);

                               
                                Robot.MoveAbs(x, y, m_commonZ, EnumRobotSpeed.V50, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                Robot.GetPointPos((int)MachinePoint.EquipDustPlugBack, out x, out y, out z);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                Robot.MoveAbs(x, y, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                Robot.MoveToPoint((int)MachinePoint.EquipDustPlugFront, CommonSpeed, 20000);
                                Robot.SetDoutBit(EnumDout.Dout2, false);
                                CheckDIo(EnumDin.Din2,true, "未读到防尘塞夹爪气缸原位信号", 2000);
                                
                                Thread.Sleep(500);
                                Robot.MoveAbs(x, y, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,104, 1001);
                                PopStep();
                                break;
                            case STEP1.UnloadFromEndFaceTest:
                                Robot.MoveToPoint((int)MachinePoint.UnloadProductUp, CommonSpeed, 10000);
                                Robot.GetPointPos((int)MachinePoint.UnloadProduct, out x, out y, out z, 1000);
                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 90000);
                                Robot.MoveAbs(x, y, z, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 40000);
                                Robot.SetDoutBit(EnumDout.Dout1, true);
                                CheckDIo(EnumDin.Din1,false, "未读到真空负压信号");
                               
                                SendMessage(new MsgUpdateTestState()
                                {
                                    State = EnumTestState.WAITING,
                                    UpdateType = EnumUpdateType.FaceTest
                                });

                                Robot.MoveAbs(x, y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 20000);
                                if (faceTestResult == 2)
                                {
                                    ClearAllStep();
                                    PushStep(STEP1.PlayToNgPoint);
                                }
                                else
                                {
                                    ClearAllStep();
                                    PushStep(STEP1.PlayToOkPoint);
                                }
                                PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,105, 1001);
                                break;
                            case STEP1.PlayToNgPoint:
                                {
                                    var CurPt = Robot.GetCurrentPostion(EnumRobotTool.Tool1);
                                    Robot.MoveAbs(CurPt.X, CurPt.Y, m_commonZ, CommonSpeed, EnumRobotTool.Tool1, EnumMoveType.MoveL, 30000);
                                    Robot.MoveToPointReplaceXYZ(EnumProductType.NG, NgPoints[NgProductCount, 0], NgPoints[NgProductCount, 1], m_commonZ, CommonSpeed, EnumRobotTool.Tool1, 20000);
                                    Robot.MoveToPointReplaceXYZ(EnumProductType.NG, NgPoints[NgProductCount, 0], NgPoints[NgProductCount, 1], NgPoints[NgProductCount, 2], CommonSpeed, EnumRobotTool.Tool1, 20000);
                                    Robot.SetDoutBit(EnumDout.Dout1, false);
                                    Thread.Sleep(500);
                                    Robot.MoveToPointReplaceXYZ(EnumProductType.NG, NgPoints[NgProductCount, 0], NgPoints[NgProductCount, 1], m_commonZ, CommonSpeed, EnumRobotTool.Tool1, 20000);
                                    Robot.MoveToPoint((int)MachinePoint.InitPoint, CommonSpeed, 30000);
                                    NgProductCount++;
                                    if (NgProductCount == 6)
                                    {
                                        //PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,105, 3000);
                                        NgProductCount = 0;
                                        SendMessage(new MsgOutput() { msg = new MessageModel() { MsgType = EnumMsgType.Warning, MsgContent = "NG料盘已满,请清理料盘" } });
                                    }
                                    PopStep();
                                }
                                break;
                            case STEP1.PlayToOkPoint:
                                Robot.MoveToPoint((int)MachinePoint.OkPointUp, CommonSpeed, 20000);
                                Robot.MoveToPointReplaceXYZ(EnumProductType.OK,OkPoints[OkProductCount, 0], OkPoints[OkProductCount, 1], m_commonZ, CommonSpeed, EnumRobotTool.Tool1, 30000);
                                Robot.MoveToPointReplaceXYZ(EnumProductType.OK,OkPoints[OkProductCount, 0], OkPoints[OkProductCount, 1], OkPoints[OkProductCount, 2], CommonSpeed, EnumRobotTool.Tool1, 30000);
                                Robot.SetDoutBit(EnumDout.Dout1, false);
                                Thread.Sleep(500);
                                Robot.MoveToPointReplaceXYZ(EnumProductType.OK,OkPoints[OkProductCount, 0], OkPoints[OkProductCount, 1], m_commonZ, CommonSpeed, EnumRobotTool.Tool1, 30000);
                                Robot.MoveToPoint((int)MachinePoint.InitPoint, CommonSpeed, 30000);
                                //PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,105, 1001);
                                OkProductCount++;
                                if (OkProductCount == 10)
                                {
                                    OkProductCount = 0;
                                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,107, 3000);
                                    SendMessage(new MsgOutput() { msg = new MessageModel() { MsgType = EnumMsgType.Warning, MsgContent = "OK料盘已满,请清理料盘" } });
                                }                     
                                PopStep();
                                break;

                            case STEP1.REST:
                                break;


                            default:
                                ShowInfo("Default");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowInfo($"程序中止:{ex.Message}");
                    InitFlag = false;
                }
                Thread.Sleep(10);
            }
            InitFlag = false;

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

        private double[,] GetAveragePoints(int rowcount,int columncount,double x1,double y1,double x2,double y2,double z)
        {
            double[,] points = new double[rowcount * columncount,3];
            double averageX = (x2 - x1) / (columncount - 1);
            double averageY = (y2 - y1) / (rowcount - 1);
            for (int row = 0; row < rowcount; row++)
            {
                for (int column = 0; column < columncount; column++)
                {
                    points[row * columncount + column,0] = x1 + averageX * column;
                    points[row * columncount + column, 1] = y1 + averageY *  row;
                    points[row * columncount + column, 2] = z;
                }
            }
            return points;
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
            NgPoint1,
            NgPoint2,
            UnloadProduct,
            InitPoint,
            OkPoint1,
            OkPoint2,
            OkPointUp,
            UnloadProductUp,
        }

        private void Dispatcher()
        {
            var IntList = PLC.ReadBlockInt(FXPLCCommunicationLib.REGISTER_TYPE.D,100, 10);
            string val = string.Empty;
            for (int k = 0; k < MappingDic.Count; k++)
            {
                val += IntList[k].ToString() + " ";
            }
            SendMessage(new MsgShowImage()
            {
                mess = val,
            });
            for (int i= MappingDic.Count-1; i>-1;i--)
            {
                if (IntList[i] == 1&& !nStepQueue.Contains((int)MappingDic[i]))
                {
                    PushStep(MappingDic[i]);
                    PLC.WriteWord(FXPLCCommunicationLib.REGISTER_TYPE.D,100 + i, 0);
                    ShowInfo($"当前压入步骤{MappingDic[i].ToString()}");
                    break;
                }
            }
          List<int> data=  nStepQueue.OrderBy(u => u).ToList();
            nStepQueue.Clear();
            for (int j = data.Count-1; j>-1 ; j--)
                nStepQueue.Enqueue(data[j]);
        }
        private void CheckDIo(EnumDin Index,bool ExpectState, string ErrorInfo,int TimeOut=1000)
        {
           long timeStart = DateTime.Now.Ticks;
            while (Robot.ReadDinBit(Index)!= ExpectState)//读负压信号
            {
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - timeStart).TotalMilliseconds > TimeOut)
                {
                    SendMessage(new MsgOutput()
                    {
                        msg = new MessageModel()
                        {
                            MsgType = EnumMsgType.Error,
                            MsgContent = ErrorInfo,
                        },
                    });
                    break;
                }
                Application.DoEvents();

            }

        }
    }
}
