using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
namespace SymcodeConmunicationLib
{
    public class Symcode1DDecoder
    {
        #region Field
        SerialPort Comport = new SerialPort();
        object ComportLock = new object();
        int m_outTime = 2000;
        #endregion
        #region UserAPI
        public bool Open(int Port)
        {
            Comport.PortName = $"COM{Port}";
            Comport.BaudRate = 9600;
            Comport.Parity = Parity.None;
            Comport.DataBits = 8;
            Comport.StopBits = StopBits.One;
            Comport.ReadTimeout = 1000;
            Comport.WriteTimeout = 1000;
            Comport.ReadBufferSize = 1024;
            Comport.WriteBufferSize = 1024;
            if (Comport.IsOpen)
                Comport.Close();
            Comport.Open();
            return Comport.IsOpen;
        }

        public void CLose()
        {
            Comport.Close();
        }
        public bool IsOpen()
        {
            return Comport.IsOpen;
        }
        public string Decode()
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            Comport.Write(new byte[4] { 0x02, 0x82, 0x03, 0x83 },0,4);
            return ReadStrAck();
        }
        /// <summary>
        /// 读取int的返回值
        /// </summary>
        /// <returns></returns>
        string ReadStrAck()
        {
            var StartTime = DateTime.Now.Ticks;          
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    int bytes = Comport.BytesToRead;
                    byte[] buffer = new byte[bytes];
                    Comport.Read(buffer, 0, bytes);
                    return Encoding.Default.GetString(buffer);
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 2000)
                    throw new Exception("通信超时");
            }
        }
        /// <summary>通信超时时间
        /// </summary>
        public int Timeout
        {
            get { return m_outTime; }
            set { m_outTime = value; }
        }
        #endregion

        public enum CMD
        {
            STX = 0x02,
            R = 0x30,
            W = 0x31,
            ETX = 0x03,
            ACK = 0x06,
            NCK = 0x15,
        };
    }
}
