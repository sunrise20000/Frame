using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data;
using System.IO;

namespace Frame.Model
{
    class StationTest : StationBase
    {
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
            PushStep(STEP.INIT);
            while (!cts.IsCancellationRequested)
            {
                if (CheckStationStatusChanged())
                    continue;
                nStep = PeekStep();
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
