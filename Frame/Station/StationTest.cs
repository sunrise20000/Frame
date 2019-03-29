using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data;
using System.IO;
using Frame.Definations;

namespace Frame.Model
{
   
    class StationTest : StationBase
    {
        STEP1 nStep;
        public StationTest()
        {
           
        }
        protected override bool UserInit()
        {
            Console.WriteLine("Getit");
            ShowInfo("Hello");
            return true;
        }
        protected override int WorkFlow()
        {
            ClearAllStep();
            PushStep(STEP1.INIT);
            while (!cts.IsCancellationRequested)
            {
                if (CheckStationStatusChanged())
                    continue;
                nStep = PeekStep<STEP1>();
                switch (nStep)
                {
                    default:
                        ShowInfo("Default");
                        break;
                        //拍照取料
                        
                        //Socket测试
                        //socket之后取料
                        //断面检测测试
                        //夹取安装防尘帽
                        //取料

                }
            }
            return 0;
        }

        private void SnapAndPick()
        {

        }

        private void SocketTest()
        {

        }

        private void PickAfterSocket()
        {

        }
    }
}
