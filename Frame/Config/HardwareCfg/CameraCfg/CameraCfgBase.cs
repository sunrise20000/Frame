using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionLib;
using static VisionLib.VisionDefinitions;

namespace Frame.Config.HardwareCfg.CameraCfg
{
    public class CameraCfgBase
    {
        EnumCamType connectType=EnumCamType.GigEVision2;
        public bool Enable { get; set; }
        public string Name { get; set; }

        public string NameForVision { get; set; }

        public int LightPortChannel { get; set; }
        public int LightValue { get; set; }

        public string ConnectType
        {
            set {
                Enum.TryParse(value, out connectType);
            }
            get { return connectType.ToString(); }
        }
    }
}
