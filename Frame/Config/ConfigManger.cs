using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Config
{
    
    /// <summary>
    /// 提供硬件的访问入口
    /// </summary>
    public class ConfigManger
    {
        readonly string FILE_COMMUNICATION = @"Config\Communication.json";
        readonly string FILE_HARDWARE = @"Config\HardwareCfg.json";
        private ConfigManger() {

        }
        private static readonly Lazy<ConfigManger> _instance = new Lazy<ConfigManger>(() => new ConfigManger());
        public static ConfigManger Instance
        {
            get { return _instance.Value; }
        }

        public void LoadConfig()
        {

        }
    }
}
