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

        STEP1 nStep;
        public StationTest()
        {
            Robot.Open();
        }
        ~StationTest()
        {
            Robot.Close();
        }
        protected override bool UserInit()
        {
            Console.WriteLine("Getit");
                     
            var ret = Robot.GetCurrentPostion(ABBRobotLib.Definations.EnumRobotTool.Tool0);
            ShowInfo($"{ret}");
            return true;
        }
        protected override int WorkFlow()
        {
            ClearAllStep();
            PushStep(STEP1.INIT);
            while (!cts.IsCancellationRequested)
            {
                if (CheckStationStatusChanged())
                    continue;
                nStep = PeekStep<STEP1>();
                switch (nStep)
                {
                    default:
                        ShowInfo("Default");
                        break;
                        //拍照取料
                        
                        //Socket测试
                        //socket之后取料
                            
                        //断面检测测试
                        //夹取安装防尘帽
                        //取料
                }
            }
            

            return 0;
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
    }
}
