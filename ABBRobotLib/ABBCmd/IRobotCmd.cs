using ABBRobotLib.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBCmd
{
    public interface IRobotCmd
    {
        EnumRobotCmd I_Cmd { get; set; }
        object GenEmptyCmd();
    }
}
