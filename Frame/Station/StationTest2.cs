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

    class StationTest2 : StationBase
    {
        STEP3 nStep;
        public StationTest2()
        {
           
        }
        protected override bool UserInit()
        {
            ShowInfo("Test2",true);
            return true;
        }
        protected override int WorkFlow()
        {
            ClearAllStep();
            PushStep(STEP3.INIT);
            while (!cts.IsCancellationRequested)
            {
                if (CheckStationStatusChanged())
                    continue;
                nStep = PeekStep<STEP3>();
                switch (nStep)
                {
                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}
