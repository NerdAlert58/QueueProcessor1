﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor1.Objects
{
    public class ProcResults
    {
        public float AverageTurnAroundTime { get; set; }
        public float AverageWaitTime { get; set; }
        public float CPUUtilization { get; set; }
    }
}