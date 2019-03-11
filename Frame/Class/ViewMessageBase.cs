using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frame.Class
{
    public class ViewMessageBase
    {
        public string MessageName
        {
            get { return GetType().Name; }
        }
    }
}
