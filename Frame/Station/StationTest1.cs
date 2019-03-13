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
    class StationTest1 : StationBase
    {
        STEP2 nStep;
        public StationTest1()
        {
           
        }
        protected override bool UserInit()
        {
            Console.WriteLine("Getit");
            ShowInfo("Test1",true);
            return true;
        }
        protected override int WorkFlow()
        {
            ClearAllStep();
            PushStep(STEP2.INIT.GetHashCode());
            while (!cts.IsCancellationRequested)
            {
                if (CheckStationStatusChanged())
                    continue;
                nStep = PeekStep<STEP2>();
                switch (nStep)
                {
                    default:
                        break;
                }
            }
            return 0;
        }
    }
}
