using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FXPLCCommunicationLib;

namespace Frame.Instrument
{
    public class InstrumentFxPLC : InstrumentBase<InstrumentCfgBase, CommunicationCfgBase>
    {


        public event EventHandler<string> RegisterDataChanged;
        
        public InstrumentFxPLC(InstrumentCfgBase InstrumentCfg, CommunicationCfgBase CommunicationCfg) : base(InstrumentCfg, CommunicationCfg)
        {
            
        }

        private FXPLCCommunicationLib.FxPLC PLC { get; } = new FXPLCCommunicationLib.FxPLC();

        public void Open()
        {
            PLC.Open(int.Parse((CommunicationCfg as ComportCfg).Port.Replace("COM", "")));
        }
        public bool IsOpen {
            get { return PLC.IsOpen(); }
        }
        public void Close()
        {
            PLC.CLose();
        }

        public short ReadWord(REGISTER_TYPE RegisterType, int RegisterNumber)
        {
            return PLC.ReadInt(RegisterType, RegisterNumber);
        }

        public Int32 ReadDword(REGISTER_TYPE RegisterType, int RegisterNumber)
        {
            return PLC.ReadDint(RegisterType, RegisterNumber);
        }

        public bool WriteWord(REGISTER_TYPE RegisterType, int RegisterNumber,Int16 Value)
        {
            return PLC.WriteInt(RegisterType, RegisterNumber, Value);
        }
        public bool WriteDword(REGISTER_TYPE RegisterType, int RegisterNumber, Int32 Value)
        {
            return PLC.WriteDint(RegisterType, RegisterNumber, Value);
        }

        public bool ForceMBit(REGISTER_TYPE RegisterType, int RegisterNumber, bool Value)
        {
            return PLC.ForceMBit(RegisterType,RegisterNumber, Value);
        }

        public void StartHeartBeat(REGISTER_TYPE RegisterType, int RegisterNumber, Int16 Value)
        {
            PLC.StartHeartBeat(RegisterType, RegisterNumber,Value);
        }

        public Int16[] ReadBlockInt(REGISTER_TYPE RegisterType, int RegisterNumber, int Length,int TimeOut=3000)
        {
            return PLC.ReadIntBlock(RegisterType, RegisterNumber, Length,TimeOut);
        }

        public bool WriteMbit(REGISTER_TYPE RegisterType, int RegisterNumber, bool Value)
        {
            return PLC.ForceMBit(RegisterType, RegisterNumber, Value);
        }
    }
}
