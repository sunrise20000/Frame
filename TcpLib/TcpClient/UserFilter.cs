using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpLib.TcpClient
{
    public class UserFilter : TerminatorReceiveFilter<StringPackageInfo>
    {
        //通知
        public event EventHandler<string> OnPackageReceived;
        public UserFilter(string TerminalString) : base(Encoding.ASCII.GetBytes(TerminalString)) // package terminator
        {
        }
        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            long len = bufferStream.Length;
            string StrRecv = bufferStream.ReadString((int)len, Encoding.ASCII);
            OnPackageReceived?.Invoke(this, StrRecv);
            bufferStream.Clear();
            return null;
        }
    }
}
