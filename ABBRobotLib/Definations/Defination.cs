using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBRobotLib.Definations
{
    public enum EnumRobotCmd
    {
        MOVEXYZ,
        ROTATE,
        GETCURPOSXYZ,
        MOVETOPOINT,
        MOVETOPOINTREPLACEXYZ,
        READDIN,
        READDOUT,
        WRITEDOUT,

        GETPOINTPOS,
        NONE = 999,
    }

    public enum EnumDin
    {
        Din1,
        Din2,
        Din3,
        Din4,
        Din5,
        Din6,
        Din7,
        Din8,
        Din9,
        Din10,
        Din11,
        Din12,
        Din13,
        Din14,
        Din15,
        Din16,
    }

    public enum EnumDout
    {
        Dout1,
        Dout2,
        Dout3,
        Dout4,
        Dout5,
        Dout6,
        Dout7,
        Dout8,
        Dout9,
        Dout10,
        Dout11,
        Dout12,
        Dout13,
        Dout14,
        Dout15,
        Dout16,
    }
    public enum EnumRobotSpeed
    {
        V5 = 1,
        V10,
        V20,
        V30,
        V40,
        V50,
        V60,
        V80,
        V100,
        V150,
        V200,
        V300,
        V400,
        V500,
        V600,
        V800,
        V1000,
        V1500,
        V2000,
    }
    public enum EnumRobotTool
    {
        Tool0 = 1,
        Tool1,
        Tool2,
        Tool3,
        Tool4,
        Tool5,
    }
    public enum EnumMoveType
    {
        MoveL,
        MoveJ,
    }
    public enum EnumProductType
    {
        OK=1,
        NG,
    }


}
