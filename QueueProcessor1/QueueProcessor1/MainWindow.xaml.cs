using QueueProcessor1.Objects;
using QueueProcessor1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace QueueProcessor1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int value = 0;
        IDictionary<int, Event> globalEvents;
        public MainWindow()
        {
            InitializeComponent();

            Console.WriteLine("Hold here.");
        }

        private void buttonCalc(object sender, RoutedEventArgs e)
        {       

               var procList = new List<Proc>()
               {
                   new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 40, Burst = 15, Arrival = 0},
                   new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 30, Burst = 25, Arrival = 25},
                   new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 30, Burst = 20, Arrival = 30},
                   new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 35, Burst = 15, Arrival = 50},
                   new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 5, Burst = 15, Arrival = 100},
                   new Proc() { Name = "P6", Color = Objects.Color.orange, Priority = 10, Burst = 10, Arrival = 105}
               };
            var handler = new Handler(procList);

            var (events, results) = handler.DoWork();
            buttoncalc.Visibility = Visibility.Hidden;
            globalEvents = events;
            Drawing(globalEvents);
        }
        private void Drawing(IDictionary<int, Event> events)
        {

            if (value < 0)
            {
                value = 0;
            }
            else if (value >= events.Keys.Count)
            {
                value = events.Values.Count-1;
            }

                burst1.Text = events[value].Processes[0].Burst.ToString();
                burst2.Text = events[value].Processes[1].Burst.ToString();
                burst3.Text = events[value].Processes[2].Burst.ToString();
                burst4.Text = events[value].Processes[3].Burst.ToString();
                burst5.Text = events[value].Processes[4].Burst.ToString();
                burst6.Text = events[value].Processes[5].Burst.ToString();

                qRemaining.Content = events[value].TimeQuantum.ToString();
                currentProcess.Content = events[value].CurrentProc.Name.ToString();

                timeBox.Content = value.ToString();
                string tmpstring = "";

                if (events[value].Finished != null)
                {
                    foreach (var x in events[value].Finished)
                    {
                        tmpstring += x.Name.ToString();
                        tmpstring += ", ";
                    }
                }
                finishedProcess.Content = tmpstring;
                tmpstring = "";
                if (events[value].Waiting != null)
                {
                    foreach (var x in events[value].Waiting)
                    {
                        tmpstring += x.Name.ToString();
                        tmpstring += ", ";
                    }
                }
                waitingProcess.Content = tmpstring;

            
        }
        private void IndexAdjust1(object sender, RoutedEventArgs e)
        {
            value += 1;
            Drawing(globalEvents);
        }
        private void IndexAdjust5(object sender, RoutedEventArgs e)
        {
            value += 5;
            Drawing(globalEvents);
        }
        private void IndexAdjust10(object sender, RoutedEventArgs e)
        {
            value += 10;
            Drawing(globalEvents);
        }
        private void IndexAdjustm1(object sender, RoutedEventArgs e)
        {
            value += -1;
            Drawing(globalEvents);
        }
        private void IndexAdjustm5(object sender, RoutedEventArgs e)
        {
            value += -5;
            Drawing(globalEvents);
        }
        private void IndexAdjustm10(object sender, RoutedEventArgs e)
        {
            value += -10;
            Drawing(globalEvents);
        }
    }
}
