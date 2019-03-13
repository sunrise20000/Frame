using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpLib.TcpClient
{
   
    public class TcpClient
    {
        EasyClient client = new EasyClient();
        string IP = "127.0.0.1";
        int Port = 0;
        public bool Connect(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
            return false;
        }

    }
}
