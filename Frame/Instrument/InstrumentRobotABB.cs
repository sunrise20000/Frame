using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg.InstrumentCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Instrument
{
    public class InstrumentRobotABB : InstrumentBase<InstrumentCfgBase,CommunicationCfgBase>
    {
        public InstrumentRobotABB(AbbRobotCfg InstrumentCfg, EthernetCfg CommunicationCfg) : base(InstrumentCfg, CommunicationCfg)
        {
            Robot.IP = CommunicationCfg.IP;
            Robot.Port = CommunicationCfg.Port;
        }
        public ABBRobotLib.AbbRobot Robot { get; } = new ABBRobotLib.AbbRobot();

    }
}
