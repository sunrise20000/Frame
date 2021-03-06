﻿using System;
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
                }
            }
            return 0;
        }
    }
}
