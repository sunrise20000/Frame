using Frame.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frame.Interface
{
    interface ICommandAction
    {
        void SendMessage<T>(T msg) where T : ViewMessageBase;
    }
}
