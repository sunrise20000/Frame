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

namespace Frame.Model
{

    class StationReadAlarm : StationBase
    {
        STEP2 nStep;
        Int32 OldAlarm = 0;
        InstrumentFxPLC PLC = null;
        readonly string PLCNAME = "FX3UPLC";
        List<string> AlarmInfoList =null;
        public StationReadAlarm()
        {
            AlarmInfoList = new List<string>()
            {
                "Socket进料气缸伸出超时(60)",
                "Socket进料气缸返回超时(61)",
                "Socket退料气缸伸出超时(62)",
                "Socket退料气缸返回超时(63)",
                "端面测试拔帽气缸伸出超时(64)",
                "端面测试拔帽气缸返回超时(65)",
                "端面测试拔帽夹爪夹紧超时(66)",
                "端面测试拔帽夹爪松开超时(67)",
                "端面测试定位气缸伸出超时(68)",
                "端面测试定位气缸返回超时(69)",
                "拔帽->端面(产品)移载具伸出超时(70)",
                "端面->拔帽(产品)载具返回超时(71)",
                "端面检测仪器伸出超时(72)",
                "端面检测仪器返回超时(73)",
                "拔帽->端面(仪器)移载伸出超时(74)",
                "端面->拔帽(仪器)移载返回超时(75)",
                "防尘帽移载伸出超时(76)",
                "防尘帽移载返回超时(77)",
                "防尘帽压紧伸出超时(78)",
                "防尘帽压紧返回超时(79)",
                "机械手报错(80)",
                "急停按钮被按下(81)",
                "防尘帽未检测到，请检查是否缺料(82)",
            };
        }
        protected override bool UserInit()
        {
            PLC = InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName(PLCNAME) as InstrumentFxPLC;
            OldAlarm = 0;
            return PLC!=null;
            
        }
        protected override int WorkFlow()
        {
            ClearAllStep();
            PushStep(STEP2.INIT.GetHashCode());
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    if (CheckStationStatusChanged())
                        continue;
                    nStep = PeekStep<STEP2>();
                    switch (nStep)
                    {
                        case STEP2.INIT:
                            GetAlarm(PLC.ReadDword("D400"));

                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    
                }
                Thread.Sleep(500);
            }
            return 0;
        }

        private void GetAlarm(Int32 data)
        {
            if (OldAlarm != data)
            {
                OldAlarm = data;
                for (int i = 0; i < 32; i++)
                {
                    if (((data >> i) & 0x01) == 1)
                    {
                        if (i < AlarmInfoList.Count)
                        {
                            SendMessage(new MsgOutput()
                            {
                                msg = new MessageModel()
                                {
                                    MsgType = EnumMsgType.Error,
                                    MsgContent = AlarmInfoList[i],
                                },
                            });
                        }
                    }
                }
            }
        }
    }
}
