using QueueProcessor1.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor1.Services
{
    public class Handler
    {
        private IList<Proc> _processes { get; set; }
        private IList<Proc> _finished { get; set; }
        private int _timeQuantum { get; set; }
        private Proc _currentProc { get; set; }
        private IDictionary<int, Event> _events { get; set; }
        private int _idleTime { get; set; }
        private ProcResults _results { get; set; }
        private Proc _lastProc { get; set; }

        // a.Show the scheduling order of the processes using a Gantt chart.
        // b.What is the turnaround time for each process?
        // c.What is the waiting time for each process?
        // d.What is the CPU utilization rate


        public Handler(IList<Proc> processes)
        {
            _processes = processes ?? throw new ArgumentNullException(nameof(processes));
            _finished = new List<Proc>();
            _timeQuantum = 10;
            _currentProc = null;
            _events = new Dictionary<int, Event>();
            _idleTime = 0;
        }

        private Proc GetIdleProc()
        {
            return new Proc()
            {
                Name = "IDLE",
                Priority = 0,
                Burst = 0,
                Arrival = 0,
                Finished = false,
                FinishedAtIndex = 0
            };
        }

        public IDictionary<int, Event> GetEvents()
        {
            return _events;
        }

        public (IDictionary<int, Event>, ProcResults) DoWork()
        {
            var finished = false;
            var index = 0;
            var timeQuantum = _timeQuantum;
            var waitQueue = new List<Proc>();
            var sb = new StringBuilder();
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
                                if (!string.Equals(_currentProc.Name, "IDLE"))
                                {
                                    waitQueue.Add(_currentProc);
                                }
                                if (!arr.Finished)
                                {
                                    waitQueue.Add(arr);
                                    waitQueue.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                }
                                _lastProc = _currentProc.Clone();
                                _currentProc = null;
                            }
                        }
                        else
                        {
                            if (!arr.Finished)
                            {
                                waitQueue.Add(arr);
                                waitQueue.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                            }
                        }
                    }
                }

                if (_currentProc == null && waitQueue.Count > 0)
                {
                    _currentProc = waitQueue[0];
                    waitQueue.RemoveAt(0);
                    timeQuantum = _timeQuantum;
                }

                if (_currentProc == null || string.Equals(_currentProc.Name, "IDLE"))
                {
                    // IDLE PROCESS TIME
                    _currentProc = GetIdleProc();
                    _lastProc = _currentProc.Clone();
                    _idleTime += 1;
                    timeQuantum -= 1;
                    if (timeQuantum == _timeQuantum - 1)
                    {
                        AddToGantt(sb, index, _currentProc.Name);
                    }
                }
                else
                {
                    // use 1 unit from active proc
                    _currentProc.Burst -= 1;
                    timeQuantum -= 1;

                    if (timeQuantum == _timeQuantum - 1)
                    {
                        if (_lastProc != null)
                        {
                            if (!string.Equals(_currentProc.Name, _lastProc.Name))
                            {
                                _currentProc.LastStartProcessing = index;
                            }
                        }                        
                        AddToGantt(sb, index, _currentProc.Name);
                    }

                    if (_currentProc.Burst == 0)
                    {
                        _currentProc.Finished = true;
                        _currentProc.FinishedAtIndex = index + 1;
                    }
                }

                if (_processes.Any(x => x.Finished == true))
                {
                    for (int i = 0; i < _processes.Count; i++)
                    {
                        var proc = _processes[i];
                        if (proc.Finished)
                        {
                            if (!_finished.Contains(proc))
                            {
                                _finished.Add(proc);
                            }
                        }
                    }
                }

                // create Event object and add to events
                _events[index] = new Event()
                {
                    Index = index,
                    Processes = CloneProcs(_processes),
                    Finished = CloneProcs(_finished),
                    Waiting = CloneProcs(waitQueue),
                    CurrentProc = _currentProc.Clone(),
                    TimeQuantum = timeQuantum,
                    Gantt = sb.ToString()
                };


                // If proc still has more units && timeQuantum is not used up, set currentProc to this guy
                if (timeQuantum == 0)
                {
                    if (!string.Equals(_currentProc.Name, "IDLE"))
                    {
                        if (!_currentProc.Finished)
                        {
                            waitQueue.Add(_currentProc);
                            waitQueue.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                        }
                    }
                    _lastProc = _currentProc.Clone();
                    _currentProc = null;
                    timeQuantum = _timeQuantum;
                }

                if (_currentProc != null && _currentProc.Finished)
                {
                    _lastProc = _currentProc.Clone();
                    _currentProc = null;
                    timeQuantum = _timeQuantum;
                }


                if (_processes.All(x => x.Finished == true))
                {
                    finished = true;
                    AddToGantt(sb, index + 1, "Complete|");
                    var lastIndex = _events.Keys.Max();
                    var lastEvent = _events[lastIndex];
                    lastEvent.Gantt = sb.ToString();
                }


                index++;
            }

            if (_events == null || _events.Count <= 0)
            {
                return (null, null);
            }
            var last = _events.Keys.Max();

            var processes = _events[last].Processes;
            var waitTime = 0;
            var turnAroundTime = 0;
            var sbTurn = new StringBuilder();
            var sbWait = new StringBuilder();

            for (int i = 0; i < processes.Count; i++)
            {
                var process = processes[i];
                process.FinalizeValues();
                waitTime += process.WaitTime;
                turnAroundTime += process.TurnAroundTime;
                sbTurn.AppendLine($"{process.Name}: {process.TurnAroundTime} units");
                sbWait.AppendLine($"{process.Name}: {process.WaitTime} units");
            }

            _results = new ProcResults()
            {
                AverageTurnAroundTime = (float)turnAroundTime / processes.Count,
                AverageWaitTime = (float)waitTime / processes.Count,
                CPUUtilization = (float)(last-_idleTime) / last,
                WaitTimes = sbWait.ToString(),
                TurnAroundTimes = sbTurn.ToString()
            };

            return (_events, _results);
        }

        private void AddToGantt(StringBuilder sb, int index, string name)
        {
            sb.Append($"|{index} - {name} ");
        }

        private IList<Proc> CloneProcs(IList<Proc> procs)
        {
            var clones = new List<Proc>();
            for (int i = 0; i < procs.Count; i++)
            {
                var proc = procs[i];
                clones.Add(proc.Clone());
            }
            return clones;
        }
    }
}
