using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpLib.TcpClient
{
   
    public class TcpClient
    {
        EasyClient Client = new EasyClient();
        string IP = "127.0.0.1";
        int Port = 0;
        UserFilter Filter = null;
        public event EventHandler<string> OnPackageRecieved;
        public event EventHandler OnConnected;
        public event EventHandler OnClosed;
        public TcpClient(string TerminalString)
        {
            Filter = new UserFilter(TerminalString);
            Client.Initialize(Filter, new Action<StringPackageInfo>(info=> {
            }));
            Filter.OnPackageReceived += Filter_OnPackageReceived;
            Client.Connected += Client_Connected;
            Client.Closed += Client_Closed;
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            OnClosed?.Invoke(sender, e);
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            OnConnected?.Invoke(sender, e);
        }

        private void Filter_OnPackageReceived(object sender, string e)
        {
            OnPackageRecieved?.Invoke(this, e);
        }

        public bool Connect(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
            return  Client.ConnectAsync(new IPEndPoint(IPAddress.Parse(this.IP), Port)).Result;
        }
        public void Send(byte[] data)
        {
            Client.Send(data);
        }

        public void Close()
        {
            Client.Close();
        }

        public bool IsConnected {
            get { return Client.IsConnected; }
        }

    }
}
