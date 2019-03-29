using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymcodeConmunicationLib;
namespace Frame.Instrument
{
    public class InstrumentScanner : InstrumentBase<InstrumentCfgBase, CommunicationCfgBase>
    {
        int Port;
        public InstrumentScanner(InstrumentCfgBase InstrumentCfg, ComportCfg CommunicationCfg):base(InstrumentCfg, CommunicationCfg)
        {
            Port = int.Parse(CommunicationCfg.Port.ToUpper().Replace("COM",""));
        }
        public Symcode1DDecoder Scanner { get; set; } = new Symcode1DDecoder();

        public bool Open()
        {
            return Scanner.Open(Port);
        }

        public void Close()
        {
            Scanner.CLose();
        }

        public bool IsOpen { get {
                return Scanner.IsOpen();
            } }

        public string Decode(int Timeout)
        {
            Scanner.Timeout = Timeout;
           return Scanner.Decode();
        }
    }
}
