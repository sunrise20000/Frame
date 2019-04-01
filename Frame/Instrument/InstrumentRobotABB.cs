using ABBRobotLib.ABBData;
using ABBRobotLib.Definations;
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
        string IP;
        int Port;
        public InstrumentRobotABB(InstrumentCfgBase InstrumentCfg, EthernetCfg CommunicationCfg) : base(InstrumentCfg, CommunicationCfg)
        {
            Robot.IP = CommunicationCfg.IP;
            Robot.Port = CommunicationCfg.Port;
            this.IP = CommunicationCfg.IP;
            this.Port = CommunicationCfg.Port;
        }
        public ABBRobotLib.AbbRobot Robot { get; } = new ABBRobotLib.AbbRobot();

        public bool Open()
        {
            return Robot.Open(IP,Port);
        }

        public bool IsOpen
        {
            get { return Robot.IsOpen; }
        }

        public void Close()
        {
            Robot.Close();
        }

        
        public bool MoveAbs(double x, double y, double z, EnumRobotSpeed speed, EnumRobotTool tool, EnumMoveType MoveType = EnumMoveType.MoveL, int TimeOut = 3000)
        {
            return Robot.MoveAbs(x,y,z,speed,tool,MoveType);
        }

        public bool MoveRel(double x, double y, double z, EnumRobotSpeed speed, EnumRobotTool tool, EnumMoveType MoveType = EnumMoveType.MoveL, int TimeOut = 3000)
        {
            return Robot.MoveRel(x, y, z, speed, tool, MoveType);
        }

        public AbbPoint GetCurrentPostion(EnumRobotTool tool, int TimeOut = 3000)
        {
            return Robot.GetCurrentPostion(tool,TimeOut);
        }

        public bool Rotate(double rx, double ry, double rz, EnumRobotSpeed speed, EnumRobotTool tool, int TimeOut = 3000)
        {
            return Robot.Rotate(rx,ry,rz,speed,tool,TimeOut);
        }

        public bool MoveToPoint(int PointID, int TimeOut=3000)
        {
            return Robot.MoveToPoint(PointID,TimeOut);
        }
    }
}
