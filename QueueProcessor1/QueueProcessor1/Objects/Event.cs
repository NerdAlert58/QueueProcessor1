using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor1.Objects
{
    public class Event
    {
        public int Index { get; set; } // This is the unit of time for which this Event exists.
        public IList<Proc> Processes { get; set; } // This is the step of processes that are requesting run time.
        public IList<Proc> Waiting { get; set; } // This will be the queue that holds and drops Processes as they are available for run time again.
        public Proc CurrentProc { get; set; }
    }
}
