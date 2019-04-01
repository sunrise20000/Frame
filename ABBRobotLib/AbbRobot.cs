using ABBRobotLib.ABBCmd;
using ABBRobotLib.ABBData;
using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpLib;
namespace ABBRobotLib
{ 
    public class AbbRobot
    {

        TcpLib.TcpClient.TcpClient Client = new TcpLib.TcpClient.TcpClient("]");
        object TcpLock = new object();

        CmdCalibrate CalibrateCmd = new CmdCalibrate();
        CmdMoveToPos MoveToPosCmd = new CmdMoveToPos();
        CmdRotate RotateCmd = new CmdRotate();
        CmdGetCurPos GetCurPosCmd = new CmdGetCurPos();
        CmdTest TestCmd = new CmdTest();
        CmdMoveToPoint MoveToPointCmd = new CmdMoveToPoint();

        bool isOpen=false;

        #region Property
        public string IP { get; set; }
        public int Port { get; set; }

        public bool IsBusy { get; private set; }
        #endregion

        #region Method
        public AbbRobot()
        {
            Client.OnPackageRecieved += Client_OnPackageRecieved;
        }

        private void Client_OnPackageRecieved(object sender, string e)
        {
            var type = RobotCmdBase.GetCmdTypeFromString(e);
            RobotCmdBase cmd = null;
            switch (type)
            {
                case EnumRobotCmd.MOVEXYZ:
                    cmd = MoveToPosCmd;
                    break;
                case EnumRobotCmd.ROTATE:
                    cmd = RotateCmd;
                    break;
                case EnumRobotCmd.GETCURPOSXYZ:
                    cmd = GetCurPosCmd;
                    break;
                case EnumRobotCmd.MOVETOPOINT:
                    cmd = MoveToPointCmd;
                    break;
                default:
                    break;
            }
            if (cmd != null)
            {
                cmd.O_ReturnObj = e;
                cmd.SetMessageState();
                IsBusy = false;
            };
        }

        public bool Open(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
            isOpen = Client.Connect(IP, Port);
            return isOpen;
        }
        public void Close()
        {
            Client.Close();
            isOpen = false;
        }
        public bool IsOpen {
            get { return isOpen; }
        }
        
        public bool MoveAbs(double x, double y, double z, EnumRobotSpeed speed, EnumRobotTool tool, EnumMoveType MoveType=EnumMoveType.MoveL,int TimeOut=3000)
        {
            MoveToPosCmd.I_Speed = speed;
            MoveToPosCmd.I_Tool = tool;
            MoveToPosCmd.I_X = x;
            MoveToPosCmd.I_Y = y;
            MoveToPosCmd.I_Z = z;
            MoveToPosCmd.MoveType = MoveType;
            var cmd=ExcuteCmd(MoveToPosCmd, TimeOut);
            return cmd != null;
        }

        public bool MoveRel(double x, double y, double z, EnumRobotSpeed speed, EnumRobotTool tool, EnumMoveType MoveType=EnumMoveType.MoveL, int TimeOut = 3000)
        {
            var CurPoint=GetCurrentPostion(tool, TimeOut);
            MoveToPosCmd.I_Speed = speed;
            MoveToPosCmd.I_Tool = tool;
            MoveToPosCmd.I_X = x+CurPoint.X;
            MoveToPosCmd.I_Y = y+CurPoint.Y;
            MoveToPosCmd.I_Z = z+CurPoint.Z;
            MoveToPosCmd.MoveType = MoveType;
            var cmd = ExcuteCmd(MoveToPosCmd, TimeOut);
            return cmd != null;
        }

        public AbbPoint GetCurrentPostion(EnumRobotTool tool,int TimeOut=3000)
        {
            GetCurPosCmd.I_Tool = tool;
            var cmd = ExcuteCmd(GetCurPosCmd, TimeOut) as CmdGetCurPos;
            AbbPoint point = new AbbPoint();
            if(cmd!=null)
            {
                point.X = cmd.Q_X;
                point.Y = cmd.Q_Y;
                point.Z = cmd.Q_Z;
                return point;
            }
            return null;
        }

        public bool Rotate(double rx, double ry, double rz, EnumRobotSpeed speed, EnumRobotTool tool, int TimeOut=3000)
        {
            RotateCmd.I_Speed = speed;
            RotateCmd.I_Tool = tool;
            RotateCmd.AngleX = rx;
            RotateCmd.AngleY = ry;
            RotateCmd.AngleZ = rz;
            return ExcuteCmd(RotateCmd, TimeOut) !=null;
        }

        public bool MoveToPoint(int PointID,int TimeOut=3000)
        {
            MoveToPointCmd.I_PointID = PointID;  
            return ExcuteCmd(MoveToPointCmd, TimeOut) != null;
        }

        private RobotCmdBase ExcuteCmd(RobotCmdBase cmd,int TimeOut)
        {
            try
            {
                byte[] RecvBuffer = new byte[256];
                if (!Client.IsConnected)
                {
                    Open(IP,Port);
                }
                if (Client.IsConnected)
                {
                    cmd.ResetMessageState();
                    lock (TcpLock)
                    {
                        IsBusy = true;
                        Client.Send(cmd.ToByteArray());
                    }
                    RobotCmdBase cmdClone = cmd.GenEmptyCmd() as RobotCmdBase;
                    if (cmd.WaitCmdRecved(TimeOut))
                    {
                        if (cmd.O_ReturnObj != null)
                            cmdClone.FromString(cmd.O_ReturnObj.ToString());
                        else
                            throw new Exception($"Error Msg received:{cmd.I_Cmd.ToString()}");
                    }
                    else
                    {
                        throw new Exception($"Timeout for waiting for Message: {cmd}");
                    }
                    return cmdClone;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        #endregion

    }
}
