﻿using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public abstract class RobotCmdBase : IRobotCmd
    {
        EnumRobotSpeed i_speed;
        EnumRobotTool i_tool;
        private AutoResetEvent SyncEvent = new AutoResetEvent(false);
        public RobotCmdBase()
        {
            for (int i = 0; i < 6; i++)
                Paras.Add("");

            I_Speed = EnumRobotSpeed.V10;
            I_Tool = EnumRobotTool.Tool0;
        }
        public EnumRobotSpeed I_Speed
        {
            get { return i_speed; }
            set {
                i_speed = value;
                Paras[4] = ((int)value).ToString();
            }
        }
        public EnumRobotTool I_Tool
        {
            get { return i_tool; }
            set {
                i_tool = value;
                Paras[5]= ((int)value).ToString();
            }
        }

        public EnumRobotCmd I_Cmd { get; set; }
        public object O_ReturnObj { get; set; }
        protected List<string> Paras = new List<string>(6);
        protected abstract void SetProfile();
        protected abstract void ReadProfile();
        public virtual byte[] ToByteArray()
        {
            SetProfile();
            string strCmd = string.Format("[\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"]", I_Cmd.ToString(), Paras[0], Paras[1], Paras[2], Paras[3], Paras[4], Paras[5]);
            List<byte> ByteList = new List<byte>();
            foreach (var ch in strCmd)
            {
                ByteList.Add((byte)ch);
            }
            return ByteList.ToArray();
        }
        public void FromString(string OriginString)
        {
            var List = OriginString.Replace("]", "").Replace("[", "").Replace("\"", "").Split(',');
            if (List.Count() != 7)
                throw new Exception("Wrong number para recieved!");
            string strCmd = List[0];
        
            if (Enum.TryParse(strCmd,out EnumRobotCmd cmdOut))
            {
                I_Cmd = cmdOut;
                for (int i = 0; i < 6; i++)
                {
                    Paras[i] = List[i + 1];
                }
                ReadProfile();
            }
            else
                throw new Exception($"Wrong cmd found: {I_Cmd.ToString()}");
          
        }
        public void FromByteArray(byte[] Buffer, int len)
        {
            StringBuilder sb = new StringBuilder();
            if (Buffer.Length < len)
                return;
            for (int i = 0; i < len; i++)
            {
                sb.Append((char)Buffer[i]);
            }
            FromString(sb.ToString());
        }

        /// <summary>
        /// 将消息设置为未收到返回值状态
        /// </summary>
        public void ResetMessageState()
        {
            SyncEvent.Reset();
        }

        /// <summary>
        /// 确定收到返回值状态
        /// </summary>
        public void SetMessageState()
        {
            SyncEvent.Set();
        }

        public bool WaitCmdRecved(int MiliSecond)
        {
            return SyncEvent.WaitOne(MiliSecond);
        }

        public static EnumRobotCmd GetCmdTypeFromString(string OriginString)
        {
            var List = OriginString.Replace("]", "").Replace("[", "").Replace("\"", "").Split(',');
            if (List.Count() != 7)
                throw new Exception("Wrong number para recieved!");
            if (Enum.TryParse(List[0], out EnumRobotCmd cmd))
                return cmd;
            return EnumRobotCmd.NONE;
        }


        /// <summary>
        /// 设置移动速度与工具
        /// </summary>
        /// <param name="I_Speed">10代表V10，20代表V20</param>
        /// <param name="I_Tool"></param>
        protected void SetSpeedAndTool(EnumRobotSpeed I_Speed,EnumRobotTool I_Tool)
        {
            Paras[4] = ((int)I_Speed).ToString();
            Paras[5] = ((int)I_Tool).ToString();
        }
        public abstract object GenEmptyCmd();
    }
}
