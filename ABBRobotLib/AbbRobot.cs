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
        CmdStopRobot StopRobotCmd = new CmdStopRobot();
        CmdTest TestCmd = new CmdTest();
       
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
                case EnumRobotCmd.Calibration:
                    cmd = CalibrateCmd;
                    break;
                case EnumRobotCmd.MoveToPos:
                    cmd = MoveToPosCmd;
                    break;
                case EnumRobotCmd.Rotate:
                    cmd = RotateCmd;
                    break;
                case EnumRobotCmd.StopRobot:
                    cmd = StopRobotCmd;
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
            return Client.Connect(IP, Port).Result;
        }
        public void Close()
        {
            Client.Close();
        }

        public bool MoveAbs(double x, double y, double z, EnumRobotSpeed speed, EnumRobotTool tool, EnumMoveType MoveType)
        {
            MoveToPosCmd.I_Speed = speed;
            MoveToPosCmd.I_Tool = tool;
            MoveToPosCmd.I_X = x;
            MoveToPosCmd.I_Y = y;
            MoveToPosCmd.I_Z = z;
            MoveToPosCmd.MoveType = MoveType;
            var cmd=ExcuteCmd(MoveToPosCmd);
            return cmd != null;
        }
    
        public ABBData.AbbPoint GetCurrentPostion()
        {

            return null;
        }

        public bool Rotote(double rx, double ry, double rz, EnumRobotSpeed speed, EnumRobotTool tool)
        {
            RotateCmd.I_Speed = speed;
            RotateCmd.I_Tool = tool;
            RotateCmd.AngleX = rx;
            RotateCmd.AngleY = ry;
            RotateCmd.AngleZ = rz;
            return ExcuteCmd(RotateCmd)!=null;
        }

        private RobotCmdBase ExcuteCmd(RobotCmdBase cmd)
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
                    if (cmd.WaitCmdRecved(30000))
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
