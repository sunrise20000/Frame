using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Camera
{
   public   class CameraBase
    {
        public  bool m_IsConnected;
        public string m_Cameraname=string .Empty;
        public string m_IpAddress=string.Empty;
        public CameraConnectType m_CameraConnectType;
        /// <summary>
        /// 连接相机
        /// </summary>
        /// <returns></returns>
        public virtual void OpenCamera() { }
      
        /// <summary>
        /// 关闭相机
        /// </summary>
        public virtual void CloseCamera() { }
      
        /// <summary>
        /// 单次取像
        /// </summary>
        public virtual void SnapShot() { }
      
        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="t"></param>
        public virtual void SetExpourseTime(int t) { }
      
        /// <summary>
        /// 设置相机增益
        /// </summary>
        /// <param name="g"></param>
        public virtual void SetGain(int g) { }
      
        
    }
   public  enum CameraConnectType
    {
        GigEVision,//
        USB3Vision,//
        DirectShow//
    }
}
