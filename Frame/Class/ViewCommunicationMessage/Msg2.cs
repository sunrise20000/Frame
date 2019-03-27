using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Class.ViewCommunicationMessage
{
    public class Msg2 : ViewMessageBase
    {
        public DateTime dt { get; set; } = DateTime.Now;
    }
}
