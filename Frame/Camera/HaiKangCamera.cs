using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvCamCtrl.NET;
using DeviceSource;
using System.Runtime.InteropServices;
using HalconDotNet;
using CLCamera;

namespace Frame.Camera
{
  public   class HaiKangCamera:CameraBase
    {
        public string Mac = string.Empty;
        CameraOperator m_pOperator;
        MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        MyCamera.cbOutputdelegate ImageCallback;
        MyCamera.cbExceptiondelegate ExceptionCallback;
        public  event EventHandler<ImageEventArgs<HObject>> ImageAcquired;
        public HaiKangCamera(string cameraname, CameraConnectType cameraconnecttype)
        {
            m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_pOperator = new CameraOperator();
            this.m_Cameraname = cameraname;
            this.m_CameraConnectType = cameraconnecttype;
        }
        public override void OpenCamera()
        {
            try
            {
                InitCamera(m_Cameraname);
                this.m_IsConnected = true;
            }
            catch (Exception ex)
            {
                this.m_IsConnected = false;
                throw new Exception(m_Cameraname + "打开失败");
            }
        }
        public override void CloseCamera()
        {
            m_pOperator.Close();      
        }
        public override void SnapShot()
        {
            int nRet;

            //触发命令
            nRet = m_pOperator.CommandExecute("TriggerSoftware");
            if (CameraOperator.CO_OK != nRet)
            {
                throw new Exception(m_Cameraname+"取像失败");
            }
        }
        public override void SetExpourseTime(int t)
        {
            m_pOperator.SetFloatValue("ExposureTime", t);
        }
        public override void SetGain(int g)
        {
            m_pOperator.SetFloatValue("Gain", g);
        }

        public List<string> EnumCam()
        {
            List<string> list = new List<string>();
            MyCamera.MV_CC_DEVICE_INFO device;
            try
            {
                CameraOperator.EnumDevices(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        list.Add(gigeInfo.chUserDefinedName);
                    }
                    else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        list.Add(usbInfo.chUserDefinedName);
                    }
                }
                return list;
            }
            catch 
            {
                throw;
            }
        }
        public void SetTriggerMode(string mode)
        {
            m_pOperator.SetEnumValue("TriggerMode", 1);
            if (mode == "Off")
                m_pOperator.SetEnumValue("TriggerSource", 7);
            else if (mode == "On")
                m_pOperator.SetEnumValue("TriggerSource", 0);
            m_pOperator.StartGrabbing();
        }
        public void InitCamera(string ID)
        {
            int numberID = -1;
            MyCamera.MV_CC_DEVICE_INFO device;
            try
            {
                CameraOperator.EnumDevices(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        if (gigeInfo.chUserDefinedName == ID)
                        {
                            Mac = gigeInfo.chSerialNumber;
                           
                            numberID = i;
                            break;
                        }
                    }
                    else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        if (usbInfo.chUserDefinedName == ID)
                        {
                            Mac = usbInfo.chSerialNumber;
                            numberID = i;
                            break;
                        }
                    }
                }
                device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[numberID], typeof(MyCamera.MV_CC_DEVICE_INFO));
                //打开设备
                int nRet = -1;
                nRet = m_pOperator.Open(ref device);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception("");
                    
                }
                //设置采集连续模式
                m_pOperator.SetEnumValue("AcquisitionMode", 2);
                m_pOperator.SetEnumValue("TriggerMode", 1);
                //m_pOperator.SetPixelFormatValue(0);

                ImageCallback = new MyCamera.cbOutputdelegate(ImageOut);
                m_pOperator.RegisterImageCallBack(ImageCallback, IntPtr.Zero);
                ExceptionCallback = new MyCamera.cbExceptiondelegate(Exception);
                m_pOperator.RegisterExceptionCallBack(ExceptionCallback, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                throw new Exception(ID + "初始化失败：" + ex.Message);
            }
        }
        private void Exception(uint param1, IntPtr param2)
        {
            throw new Exception("123");
        }
        private void ImageOut(IntPtr pixelPointer, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser)
        {
            HObject objImage = new HObject();
            // 原始数据转换
            int width = pFrameInfo.nWidth;
            int height = pFrameInfo.nHeight;
            if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                HOperatorSet.GenImage1(out objImage, "byte", width, height, pixelPointer);
            }
            else if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8)
            {
                width = (width + 3) & 0xfffc;  //宽度补齐为4的倍数
                int nLength = width * height;
                byte[] dataBlue = new byte[nLength];
                byte[] dataGreen = new byte[nLength];
                byte[] dataRed = new byte[nLength];

                for (int row = 0; row < height; row++)
                {
                    //uint char* ptr = &pixelPointer[row * width * 3];
                    for (int col = 0; col < width; col++)
                    {
                        //dataBlue[row * width + col] = pixelPointer[3 * col];
                        //dataGreen[row * width + col] = pixelPointer[3 * col + 1];
                        //dataRed[row * width + col] = pixelPointer[3 * col + 2];
                    }
                }
                //objImage=new HImage("")
                //HOperatorSet.GenImage1(out objImage, "byte", width*3, height, pixelPointer);
                HOperatorSet.GenImageInterleaved(out objImage, pixelPointer, "rgb", width, height, -1, "byte", 0, 0, 0, 0, -1, 0);
            }
            ImageEventArgs<HObject> outEvent = new ImageEventArgs<HObject>();
            outEvent.image = objImage;
            
            OnNewImageAcquired(outEvent);

        }
          void OnNewImageAcquired(ImageEventArgs<HObject> e)
        {
            EventHandler<ImageEventArgs<HObject>> tempEvent = this.ImageAcquired;
            if (tempEvent != null)
            {
                tempEvent(this, e);
            }
        }

    }
}
