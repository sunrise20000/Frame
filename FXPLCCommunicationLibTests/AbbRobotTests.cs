using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABBRobotLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ABBRobotLib.Tests
{
    [TestClass()]
    public class AbbRobotTests
    {
        [TestMethod()]
        public void GetCurPosTest()
        {
            AbbRobot robot = new AbbRobot();
            robot.Open("192.168.125.1", 4000);
            for (int i = 0; i < 5; i++)
            {
                var pt = robot.GetCurrentPostion(Definations.EnumRobotTool.Tool1, 3000);
                Console.WriteLine($"{pt.X},{pt.Y},{pt.Z}");
            }
            //robot.Close();
            //Assert.Fail();
        }

        [TestMethod()]
        public void RotateTest()
        {
            AbbRobot robot = new AbbRobot();
            robot.Open("192.168.125.1", 4000);
            for (int i = 0; i < 150; i++)
            {
                var pt = robot.Rotote(0,-3,0,Definations.EnumRobotSpeed.V10,Definations.EnumRobotTool.Tool0);
                
            }
            //robot.Close();
            //Assert.Fail();
        }

        [TestMethod()]
        public void MoveXYZRelTest()
        {
            AbbRobot robot = new AbbRobot();
            robot.Open("192.168.125.1", 4000);
            for (int i = 0; i < 150; i++)
            {
                var pt = robot.MoveRel(3, 0, 0, Definations.EnumRobotSpeed.V1500, Definations.EnumRobotTool.Tool0);

            }
            //robot.Close();
            //Assert.Fail();
        }

        [TestMethod()]
        public void MoveXYZAbsTest()
        {
            AbbRobot robot = new AbbRobot();
            robot.Open("192.168.125.1", 4000);
            for (int i = 0; i < 1; i++)
            {
                var pt = robot.MoveAbs(308, 10, 298, Definations.EnumRobotSpeed.V10, Definations.EnumRobotTool.Tool0);

            }
            //robot.Close();
            //Assert.Fail();
        }
    }
}