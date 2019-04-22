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
        private IDictionary<int, Event> _events { get; set; }
        // public Proc _idleProc { get; set; }



        public Handler(IList<Proc> processes)
        {
            _processes = processes ?? throw new ArgumentNullException(nameof(processes));
            _timeQuantum = 10;
            _currentProc = null;
            _events = new Dictionary<int, Event>();
        }

        private Proc GetIdleProc()
        {
            return new Proc()
            {
                Name = "IDLE",
                Priority = 0,
                Burst = 0,
                Arrival = 0,
                Finished = false
            };
        }

        public IDictionary<int,Event> DoWork()
        {
            var finished = false;
            var index = 0;
            var timeQuantum = _timeQuantum;
            var waitQueue = new List<Proc>();
            while (!finished)
            {
                // Check for new arrivals
                var arrivals = new List<Proc>();
                if (_processes.Any(x => x.Arrival == index))
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
                            if (!arr.Finished)
                            {
                                _currentProc = arr;
                            }                            
                        }
                    }
                }

                if (_currentProc == null && waitQueue.Count > 0)
                {
                    _currentProc = waitQueue[0];
                    waitQueue.RemoveAt(0);
                }

                if (_currentProc == null || string.Equals(_currentProc.Name,"IDLE"))
                {
                    // IDLE PROCESS TIME
                    _currentProc = GetIdleProc();
                }
                else
                {
                    // use 1 unit from active proc
                    _currentProc.Burst -= 1;
                    timeQuantum -= 1;

                    if (timeQuantum == _timeQuantum - 1)
                    {
                        _currentProc.LastStartProcessing = index;
                    }

                    if (_currentProc.Burst == 0)
                    {
                        _currentProc.Finished = true;
                        _currentProc.FinishedAtIndex = index + 1;
                    }
                }
                
                // create Event object and add to events
                _events[index] = new Event()
                {
                    Index = index,
                    Processes = CloneProcs(_processes),
                    Waiting = CloneProcs(waitQueue),
                    CurrentProc = _currentProc.Clone(),
                    TimeQuantum = timeQuantum
                };

                if (!string.Equals(_currentProc.Name, "IDLE"))
                {
                    // If proc still has more units && timeQuantum is not used up, set currentProc to this guy
                    if (timeQuantum == 0)
                    {
                        if (!_currentProc.Finished)
                        {
                            waitQueue.Add(_currentProc);
                            waitQueue.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                        }
                        _currentProc = null;
                        timeQuantum = _timeQuantum;
                    }

                    if (_currentProc != null && _currentProc.Finished)
                    {
                        _currentProc = null;
                    }
                }

                if (_processes.All(x => x.Finished == true))
                {
                    finished = true;
                }
                

                index++;
            }
            return _events;
        }

        private IList<Proc> CloneProcs(IList<Proc> procs)
        {
            var clones = new List<Proc>();
            for (int i = 0; i < procs.Count; i++)
            {
                var proc = procs[i];
                clones.Add(new Proc()
                {
                    Name = proc.Name,
                    Color = proc.Color,
                    Burst = proc.Burst,
                    Arrival = proc.Arrival,
                    Finished = proc.Finished,
                    FinishedAtIndex = proc.FinishedAtIndex,
                    LastStartProcessing = proc.LastStartProcessing
                });
            }
            return clones;
        }
    }
}
