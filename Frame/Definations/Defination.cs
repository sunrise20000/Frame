using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Definations
{
    public enum EnumMsgType
    {
        Info,
        Warning,
        Error,
    }
    public enum STEP1
    {
        //station1
        Init,//初始化
        WaitInitOk,
        GrapProduct,//抓取产品
        CorrectProductPos,//矫正产品位置（角度以及xy偏移）
        ReadBarCode,//读取产品一维码

        PlayToSocket,//把产品放置到Socket测试位置

        SocketTest,

        UnLoadFromSocket,//把产品从socket测试位置拿走

        FaceTest,//端面检测


        GetDustPlug,//夹取防尘塞
        EquipDustPlug,//安装防尘塞


        UnloadFromEndFaceTest,//卸载产品
        PlayToNgPoint,//放置到ng点位
        PlayToOkPoint,//放置到ok点位
        REST,   //复位
    }
    public enum STEP2
    {
        //station1
        INIT,
    }
    public enum STEP3
    {
        //station1
        INIT,
    }

    

   

}
