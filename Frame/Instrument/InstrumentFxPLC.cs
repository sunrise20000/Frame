using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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

        public short ReadWord(string RegisterName)
        {
            return PLC.ReadInt(RegisterName);
        }

        public Int32 ReadDword(string RegisterName)
        {
            return PLC.ReadDint(RegisterName);
        }

        public bool WriteWord(string RegisterName,Int16 Value)
        {
            return PLC.WriteInt(RegisterName, Value);
        }
        public bool WriteDword(string RegisterName, Int32 Value)
        {
            return PLC.WriteDint(RegisterName, Value);
        }

        public bool ForceMBit(string MBitName, bool Value)
        {
            return PLC.ForceMBit(MBitName, Value);
        }

        public void StartHeartBeat(string RegisterName, Int16 Value)
        {
            PLC.StartHeartBeat(RegisterName,Value);
        }

        public Int16[] ReadBlockInt(string StartRegisterName, int Length,int TimeOut=3000)
        {
            return PLC.ReadIntBlock(StartRegisterName, Length,TimeOut);
        }

        public bool WriteMbit(string MName,bool Value)
        {
            return PLC.ForceMBit(MName, Value);
        }
    }
}
