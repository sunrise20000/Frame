using Microsoft.VisualStudio.TestTools.UnitTesting;
using FXPLCCommunicationLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FXPLCCommunicationLib.Tests
{
    [TestClass()]
    public class FxPLCTests
    {
        [TestMethod()]
        public void CheckWriteInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret=plc.WriteInt(FXPLCCommunicationLib.REGISTER_TYPE.D,100,-987);
            Console.WriteLine(ret);
        }

        [TestMethod()]
        public void CheckReadInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.ReadInt(FXPLCCommunicationLib.REGISTER_TYPE.D,100);
            Console.WriteLine(ret);
        }

        [TestMethod()]
        public void CheckWriteDInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.WriteDint(FXPLCCommunicationLib.REGISTER_TYPE.D,10,-87767556);
            Console.WriteLine(ret);
        }

        [TestMethod()]
        public void CheckReadDInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.ReadDint(FXPLCCommunicationLib.REGISTER_TYPE.D,10);
            Console.WriteLine(ret);
        }


        [TestMethod()]
        public void CheckReadIntBlock()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.ReadIntBlock(FXPLCCommunicationLib.REGISTER_TYPE.D,10,10);
            foreach(var v in ret)
                Console.WriteLine(v);
        }

        [TestMethod()]
        public void CheckReadM()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            plc.ForceMBit(FXPLCCommunicationLib.REGISTER_TYPE.M_SINGAL,1, true);
            var ret = plc.ReadInt(FXPLCCommunicationLib.REGISTER_TYPE.M_GROUP,0);
            plc.CLose();
        }



    }
}