using QueueProcessor1.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor1.Services {
    public class Handler
    {
        private IList<Proc> _processes { get; set; }
        private int _timeQuantum { get; set; }
        private Proc _currentProc { get; set; }



        public Handler(IList<Proc> processes)
        {
            _processes = processes ?? throw new ArgumentNullException(nameof(processes));
            _timeQuantum = 10;
            _currentProc = null;
        }

        private void DoWork()
        {
            var finished = false;
            var index = 0;
            var waitQueue = new List<Proc>();
            while (!finished)
            {
                // Check for new arrivals
                var arrivals = new List<Proc>();
                if (_processes.All(x => x.Arrival == index))
                {
                    for (int i = 0; i < _processes.Count; i++)
                    {
                        var myProc = _processes[i];
                        if (myProc.Arrival == index)
                        {
                            arrivals.Add(myProc);
                        }
                    }

                    // Check if process is currently running
                    for (int i = 0; i < arrivals.Count; i++)
                    {
                        var arr = arrivals[i];

                        if (_currentProc != null)
                        {
                            if (_currentProc.Priority >= arr.Priority)
                            {
                                waitQueue.Add(arr);
                                waitQueue.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                            }
                            else
                            {
                                waitQueue.Add(_currentProc);
                                _currentProc = arr;
                            }
                        }
                        else
                        {
                            _currentProc = arr;
                        }
                    }

                    // use 1 unit from active proc
                    _currentProc.Burst -= 1;
                    _timeQuantum -= 1;
                    
                    // If proc still has more units && timeQuantum is not used up, set currentProc to this guy
                    if (_currentProc.Burst > 0 && _timeQuantum > 0)
                    {

                    }
                    // create Event object and add to events
                }

                index++;
            }
        }
    }
}
