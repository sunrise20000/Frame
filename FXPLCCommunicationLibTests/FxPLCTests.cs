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
            var ret=plc.WriteInt("D100",-987);
            Console.WriteLine(ret);
        }

        [TestMethod()]
        public void CheckReadInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.ReadInt("D100");
            Console.WriteLine(ret);
        }

        [TestMethod()]
        public void CheckWriteDInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.WriteDint("D10",-87767556);
            Console.WriteLine(ret);
        }

        [TestMethod()]
        public void CheckReadDInt()
        {
            FxPLC plc = new FxPLC();
            plc.Open(20);
            var ret = plc.ReadDint("D10");
            Console.WriteLine(ret);
        }


        [TestMethod()]
        public void CheckReadIntBlock()
        {
            FxPLC plc = new FxPLC();
            plc.Open(19);
            var ret = plc.ReadIntBlock("D10",10);
            foreach(var v in ret)
                Console.WriteLine(v);
        }



    }
}