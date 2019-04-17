using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace FXPLCCommunicationLib
{
    public class FxPLC
    {
        #region Field
        SerialPort Comport = new SerialPort();
        object ComportLock = new object();
        Task HeartTask = null;
        CancellationTokenSource cts = null;
        bool isOpen = false;
        #endregion

        #region UserAPI
        public bool Open(int Port)
        {
            lock (ComportLock)
            {
                Comport.PortName = $"COM{Port}";
                Comport.BaudRate = 9600;
                Comport.Parity = Parity.Even;
                Comport.DataBits = 7;
                Comport.StopBits = StopBits.One;
                Comport.ReadTimeout = 1000;
                Comport.WriteTimeout = 1000;
                Comport.ReadBufferSize = 1024;
                Comport.WriteBufferSize = 1024;
                if (Comport.IsOpen)
                    Comport.Close();
                Comport.Open();
                Comport.DiscardOutBuffer();
                Comport.DiscardInBuffer();
                isOpen = Comport.IsOpen;
                return isOpen;
            }
        }

        public void CLose()
        {
            lock (ComportLock)
            {
                if (cts != null)
                    cts.Cancel();
                if (HeartTask != null)
                    HeartTask.Wait();
                Comport.Close();
            }
        }

        /// <summary>
        /// 读取D寄存器的值
        /// </summary>
        /// <param name="strRegisterName"></param>
        /// <returns></returns>
        public Int16 ReadInt(REGISTER_TYPE RegisterType, int RegisterNumber)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            if (RegisterType != REGISTER_TYPE.D && RegisterType != REGISTER_TYPE.M_GROUP)
            {
                throw new Exception("寄存器地址输入错误");
            }
            //地址计算
            AddressToAscii(RegisterType, RegisterNumber, out byte[] addressArray);
            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            var dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //byte[] dataSendFinall =new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, (byte)CMD.ETX, sum1, sum0 };
            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadIntAck();
            }

        }
        public Int32 ReadDint(REGISTER_TYPE RegisterType, int RegisterNumber)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            if (RegisterType!=REGISTER_TYPE.D && RegisterType!=REGISTER_TYPE.M_GROUP)
            {
                throw new Exception("寄存器地址输入错误");
            }
            //地址计算
            AddressToAscii(RegisterType, RegisterNumber, out byte[] addressArray);

            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x34, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            var dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //byte[] dataSendFinall =new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, (byte)CMD.ETX, sum1, sum0 };
            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadDintAck();
            }

        }
        public bool WriteInt(REGISTER_TYPE RegisterType, int RegisterNumber, int value)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            if (RegisterType!=REGISTER_TYPE.D && RegisterType!=REGISTER_TYPE.M_GROUP)
            {
                throw new Exception("寄存器地址输入错误");
            }
            //地址计算
            AddressToAscii(RegisterType, RegisterNumber, out byte[] addressArray);

            Dec2Ascii((byte)(value & 0xFF), out byte ascii0, out byte ascii1);
            Dec2Ascii((byte)((value >> 8) & 0xFF), out byte ascii2, out byte ascii3);
            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.W, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, ascii1, ascii0, ascii3, ascii2, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            List<byte> dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadVoidAck();
            }
        }
        public bool WriteDint(REGISTER_TYPE RegisterType, int RegisterNumber, Int32 value)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            if (RegisterType!=REGISTER_TYPE.D && RegisterType!=REGISTER_TYPE.M_GROUP)
            {
                throw new Exception("寄存器地址输入错误");
            }
            //地址计算
            AddressToAscii(RegisterType, RegisterNumber, out byte[] addressArray);

            Dec2Ascii((byte)(value & 0xFF), out byte ascii0, out byte ascii1);
            Dec2Ascii((byte)((value >> 8) & 0xFF), out byte ascii2, out byte ascii3);
            Dec2Ascii((byte)((value>>16) & 0xFF), out byte ascii4, out byte ascii5);
            Dec2Ascii((byte)((value >> 24) & 0xFF), out byte ascii6, out byte ascii7);

            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.W, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x34, ascii1, ascii0, ascii3, ascii2, ascii5, ascii4, ascii7, ascii6, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            List<byte> dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadVoidAck();
            }
        }

        public Int16[] ReadIntBlock(REGISTER_TYPE RegisterType, int RegisterNumber, int Length,int TimeOut=3000)
        {
            if (Length > 124)
                throw new Exception("不允许读取太多长度 的寄存器");
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            if (RegisterType!=REGISTER_TYPE.D && RegisterType!=REGISTER_TYPE.M_GROUP)
            {
                throw new Exception("寄存器地址输入错误");
            }
            //地址计算
            AddressToAscii(RegisterType, RegisterNumber, out byte[] addressArray);

            Dec2Ascii((byte)(Length * 2), out byte LenAscii0, out byte LenAscii1);

            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], LenAscii1, LenAscii0, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            var dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);
            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadBlockAck(Length, TimeOut);
            }
        }

        public bool ForceMBit(REGISTER_TYPE RegisterType, int RegisterNumber, bool Value)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            if (RegisterType!=REGISTER_TYPE.M_SINGAL)
            {
                throw new Exception("寄存器地址输入错误");
            }
            //地址计算
            AddressToAscii(RegisterType, RegisterNumber, out byte[] addressArray);
            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)(Value? CMD.FORCE_ON : CMD.FORCE_OFF), addressArray[1], addressArray[0], addressArray[3], addressArray[2],(byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            List<byte> dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);
            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadVoidAck();
            }
        }

        public bool IsOpen()
        {
            lock (ComportLock)
            {
                return Comport.IsOpen;
            }
        }


        public void StartHeartBeat(REGISTER_TYPE RegisterType, int RegisterNumber, Int16 ExpectValue, int TimeInterval=3000)
        {
            if (HeartTask==null || HeartTask.Status == TaskStatus.Canceled || HeartTask.Status == TaskStatus.RanToCompletion)
            {
                cts = new CancellationTokenSource();
                HeartTask = new Task(() => {
                    while (!cts.IsCancellationRequested)
                    {
                        if (!WriteInt(RegisterType,RegisterNumber, ExpectValue))
                            throw new Exception("PLC 断开连接");

                        Thread.Sleep(TimeInterval);
                    }
                }, cts.Token);
                HeartTask.Start();
            }
        }
        #endregion

        #region Private Method

        /// <summary>
        /// 和校验
        /// </summary>
        /// <param name="DataList"></param>
        /// <param name="sum0"></param>
        /// <param name="sum1"></param>
        void CheckSum(IEnumerable<byte> DataList, out byte sum0, out byte sum1)
        {
            int nSize = DataList.Count(), sum = 0;
            for (int i = 0; i < nSize; i++)
                sum += DataList.ElementAt(i);
            string strSum = string.Format("{0:X2}", sum & 0xFF).ToUpper();
            sum0 = (byte)strSum[1];
            sum1 = (byte)strSum[0];
        }
       
        /// <summary>
        /// 和校验
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="nStartPos"></param>
        /// <param name="nEndPos"></param>
        /// <param name="sum0"></param>
        /// <param name="sum1"></param>
        void CheckSum(IEnumerable<byte> byteArray, int nStartPos, int nEndPos, out byte sum0, out byte sum1)
        {
            if (nEndPos > byteArray.Count() || nStartPos < 0)
                throw new Exception("数组起始范围不正确，请仔细检查！");
            List<byte> list = new List<byte>();
            for (int i = nStartPos; i <=nEndPos; i++)
            {
                list.Add(byteArray.ElementAt(i));
            }
            CheckSum(list, out sum0, out sum1);
        }

        /// <summary>
        /// 将三菱PLC的地址映射转为字节数组
        /// </summary>
        /// <param name="nType"></param>
        /// <param name="dwAddress"></param>
        /// <param name="outAddress"></param>
        void AddressToAscii(REGISTER_TYPE nType, Int32 dwAddress, out byte[] outAddress)
        {
            outAddress = new byte[4];
            string strAddress = "";
            switch (nType)
            {
                case REGISTER_TYPE.D:
                    dwAddress = dwAddress * 2 + 0x1000;
                    break;
                case REGISTER_TYPE.M_GROUP:
                    dwAddress = dwAddress + 0x100;

                    break;
                case REGISTER_TYPE.M_SINGAL:
                    dwAddress = dwAddress + 0x800;
                    
                    break;
                default:
                    return;
            }
            strAddress = string.Format("{0:X4}", dwAddress).ToUpper();
            for (int i = 0; i < 4; i++)
                outAddress[3 - i] = (byte)strAddress[i];

        }

        /// <summary>
        /// 将16进制字符串转为10进制数
        /// </summary>
        /// <param name="szHexData"></param>
        /// <param name="nStartPos"></param>
        /// <param name="nEndPos"></param>
        /// <returns></returns>
        Int32 HexStr2Dec(string szHexData, int nStartPos, int nEndPos)   //仅支持大写字母
        {
            Int32 dwRet = 0;
            for (int i = nStartPos; i < nEndPos; i++)
            {
                int x = HexCh2Dec(szHexData[i]);
                var y=Math.Pow(16.0f, nEndPos - nStartPos - i - 1);
                dwRet += (Int32)((double)x * y);
            }
            return dwRet;
        }

        /// <summary>
        /// 将Hex字符转为10进制形式
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        byte HexCh2Dec(char ch)
        {
            switch (ch)
            {
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                default:
                    return byte.Parse(ch.ToString());
            }
        }

        /// <summary>
        /// 将10进制的数字，表示为16进制的字符串标示
        /// 比如10=“0A”
        /// </summary>
        /// <param name="nData"></param>
        /// <param name="ascii0"></param>
        /// <param name="ascii1"></param>
        void Dec2Ascii(byte nData, out byte ascii0, out byte ascii1)
        {
            var strData = string.Format("{0:X2}",nData);
            ascii0 = (byte)strData[1];
            ascii1 = (byte)strData[0];
        }

        /// <summary>
        /// 不需要返回值的时候的读取
        /// </summary>
        /// <returns></returns>
        bool ReadVoidAck([CallerMemberName]string CallerName="")
        {
            var StartTime = DateTime.Now.Ticks;
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch = Comport.ReadByte();
                    if (ch == (byte)CMD.ACK)
                    {
                        while (Comport.BytesToRead != 0)
                        {
                            Comport.ReadByte();
                        }
                        return true;
                    }
                    else
                    {
                        while (Comport.BytesToRead != 0)
                        {
                            Comport.ReadByte();
                        }
                        return false;
                    }
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception($"通信超时,调用者{CallerName}");
            }
        }

        /// <summary>
        /// 读取int的返回值
        /// </summary>
        /// <returns></returns>
        Int16 ReadIntAck([CallerMemberName]string CallerName="")
        {
            var StartTime = DateTime.Now.Ticks;
            List<byte> listRead = new List<byte>();
            bool IsHeaderFound = false;
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch = Comport.ReadByte();
                    if (ch == (byte)CMD.STX)
                    {
                        IsHeaderFound = true;
                    }
                    if (IsHeaderFound)
                    {
                        listRead.Add((byte)ch);
                        
                        if (listRead.Count == 6 && ch == (byte)CMD.ETX)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append((char)listRead[3]);
                            sb.Append((char)listRead[4]);
                            sb.Append((char)listRead[1]);
                            sb.Append((char)listRead[2]);
                            while (Comport.BytesToRead != 0)
                            {
                                Comport.ReadByte();
                            }
                            return Convert.ToInt16(sb.ToString(), 16);
                        }
                    }
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception($"通信超时,调用者{CallerName}");
            }
        }

        /// <summary>
        /// 读取Dint的返回值
        /// </summary>
        /// <returns></returns>
        Int32 ReadDintAck([CallerMemberName]string CallerName="")
        {
            var StartTime = DateTime.Now.Ticks;
            List<byte> listRead = new List<byte>();
            bool IsHeaderFound = false;
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch = Comport.ReadByte();
                    if (ch == (byte)CMD.STX)
                    {
                        IsHeaderFound = true;
                    }
                    if (IsHeaderFound)
                    {
                        listRead.Add((byte)ch);

                        if (listRead.Count == 10 && ch == (byte)CMD.ETX)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append((char)listRead[7]);
                            sb.Append((char)listRead[8]);
                            sb.Append((char)listRead[5]);
                            sb.Append((char)listRead[6]);

                            sb.Append((char)listRead[3]);
                            sb.Append((char)listRead[4]);
                            sb.Append((char)listRead[1]);
                            sb.Append((char)listRead[2]);
                            while (Comport.BytesToRead != 0)
                            {
                                Comport.ReadByte();
                            }
                            return Convert.ToInt32(sb.ToString(), 16);
                        }
                    }
                }

                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception($"通信超时,调用者{CallerName}");
            }
        }

        Int16[] ReadBlockAck(int ExpectLength,int TimeOut=3000,[CallerMemberName]string CallerName="")
        {
            var StartTime = DateTime.Now.Ticks;
            List<byte> listRead = new List<byte>();
            bool IsHeaderFound = false;
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch = Comport.ReadByte();
                    if (ch == (byte)CMD.STX)
                    {
                        IsHeaderFound = true;
                    }
                    if (IsHeaderFound)
                    {
                        listRead.Add((byte)ch);

                        if (listRead.Count == (ExpectLength*4+2) && ch == (byte)CMD.ETX)
                        {
                            StringBuilder sb = new StringBuilder();
                            List<Int16> list = new List<short>();
                            for (int i = 0; i < ExpectLength; i++)
                            {
                                sb.Clear();
                                sb.Append((char)listRead[i * 4+3]);
                                sb.Append((char)listRead[i * 4+4]);
                                sb.Append((char)listRead[i * 4+1]);
                                sb.Append((char)listRead[i * 4+2]);
                                list.Add(Convert.ToInt16(sb.ToString(), 16));
                            }
                            while (Comport.BytesToRead != 0)
                            {
                                Comport.ReadByte();
                            }
                            return list.ToArray();
                        }
                    }
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > TimeOut)
                    throw new Exception($"通信超时,调用者{CallerName}");
            }
        }
        #endregion
    };

}