using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frame.Config.CommunicationCfg;
using Frame.Config.HardwareCfg;
using Newtonsoft.Json;
using ABBRobotLib;
using Frame.Instrument;
using Frame.Config.HardwareCfg.InstrumentCfg;

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
        public HardwareCfgEntry HardwarecfgEntry { get; set; }
        public CommunicationCfgEntry CommunicationcfgEntry { get; set; }
        public void LoadConfig()
        {
            try
            {
                string JsonStr = File.ReadAllText(FILE_HARDWARE);
                HardwarecfgEntry=JsonConvert.DeserializeObject<HardwareCfgEntry>(JsonStr);

                JsonStr = File.ReadAllText(FILE_COMMUNICATION);
                CommunicationcfgEntry = JsonConvert.DeserializeObject<CommunicationCfgEntry>(JsonStr);


                InstanseCfg();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void InstanseCfg()
        {
            foreach (var cfg in HardwarecfgEntry.Instruments)
            {
                if (cfg.Enable && cfg.Name.Equals("RobotAbb"))
                {
                    foreach (var commuCfg in CommunicationcfgEntry.Ethernets)
                    {
                        if (commuCfg.Enable && commuCfg.PortName == cfg.PortName)
                        {
                            InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.AddInstanse(new InstrumentRobotABB(cfg, commuCfg));
                            break;
                        }
                    }
                }
                else if (cfg.Enable && cfg.Name.Equals("FX3UPLC"))
                {
                    foreach (var commuCfg in CommunicationcfgEntry.Ethernets)
                    {
                        if (commuCfg.Enable && commuCfg.PortName == cfg.PortName)
                        {
                            InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.AddInstanse(new InstrumentRobotABB(cfg, commuCfg));
                            break;
                        }
                    }
                }
                else if (cfg.Enable && cfg.Name.Equals("Scanner"))
                {
                    foreach (var commuCfg in CommunicationcfgEntry.Comports)
                    {
                        if (commuCfg.Enable && commuCfg.PortName == cfg.PortName)
                        {
                            InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.AddInstanse(new InstrumentScanner(cfg, commuCfg));
                            break;
                        }
                    }
                }
            }
        }
    }
}
