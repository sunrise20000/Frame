using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Instrument
{
    public class InstrumentFxPLC : InstrumentBase<InstrumentCfgBase, CommunicationCfgBase>
    {
        public InstrumentFxPLC(InstrumentCfgBase InstrumentCfg, CommunicationCfgBase CommunicationCfg) : base(InstrumentCfg, CommunicationCfg)
        {
            
        }

        private FXPLCCommunicationLib.FxPLC PLC { get; } = new FXPLCCommunicationLib.FxPLC();

        public void Open()
        {
            PLC.Open(int.Parse(CommunicationCfg.PortName.Replace("COM", "")));
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

    }
}
