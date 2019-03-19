using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.ABBData
{
    public class AbbPoint
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double EX { get; set; }
        public double EY { get; set; }
        public double EZ { get; set; }
    }
}
