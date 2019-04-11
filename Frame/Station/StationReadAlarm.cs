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
                "进料气缸伸出超时",
                "进料气缸返回超时   ",
                "退料气缸伸出超时   ",
                "退料气缸返回超时   ",
                "拔帽气缸伸出超时   ",
                "拔帽气缸返回超时   ",
                "拔帽夹爪伸出超时   ",
                "拔帽夹爪返回超时   ",
                "产品定位伸出超时   ",
                "产品定位返回超时   ",
                "载具移载伸出超时   ",
                "载具移载返回超时   ",
                "端面检测伸出超时   ",
                "端面检测返回超时   ",
                "端面移载伸出超时   ",
                "端面移载返回超时   ",
                "盖帽移载伸出超时   ",
                "盖帽移载返回超时   ",
                "盖帽压紧伸出超时   ",
                "盖帽压紧返回超时   ",
                "机械手报错         ",
                "急停               ",
                "盖帽产品不到位报错 ",

            };
        }
        protected override bool UserInit()
        {
            PLC = InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName(PLCNAME) as InstrumentFxPLC;
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
