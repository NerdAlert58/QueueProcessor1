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
using QueueProcessor1.Objects;

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

            /*       var processes = new List<Proc>()
                   {
                       new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 40, Burst = 15, Arrival = 0},
                       new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 30, Burst = 25, Arrival = 25},
                       new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 30, Burst = 20, Arrival = 30},
                       new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 35, Burst = 15, Arrival = 50},
                       new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 5, Burst = 15, Arrival = 100},
                       new Proc() { Name = "P6", Color = Objects.Color.orange, Priority = 10, Burst = 10, Arrival = 105}
                   };*/

            var processes = new List<Proc>()
            {
                new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 40, Burst = 15, Arrival = 0, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 30, Burst = 25, Arrival = 25, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 30, Burst = 20, Arrival = 30, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 35, Burst = 15, Arrival = 50, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 5, Burst = 15, Arrival = 100, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P6", Color = Objects.Color.orange, Priority = 10, Burst = 10, Arrival = 105, TurnAroundTime = -1, WaitTime = -1}
            };

            /*   for (int j = 0; j < 15; j++)
               {
                   Button MyControl1 = new Button();
                   MyControl1.Content =j.ToString();
                   MyControl1.Name = "Button" + j.ToString();
                   MyControl1.SetValue(Grid.ColumnProperty, j);
                   MyControl1.Height = 35;
                   queuegrid.ColumnDefinitions.Add(new ColumnDefinition());
                   queuegrid.Children.Add(MyControl1);

               }*/
            Console.WriteLine("Hold here.");
        }

        private void buttonCalc(object sender, RoutedEventArgs e)
        { /*            
            Proc p0 = new Proc();
            p0.Burst = Convert.ToInt32(burst1.Text);
            p0.Priority = Convert.ToInt32(priority1.Text);
            p0.Arrival = Convert.ToInt32(arrival1.Text);
            p0.Name = "P0";
            p0.Color = Objects.Color.blue;

            Proc p1 = new Proc();
            p1.Burst = Convert.ToInt32(burst2.Text);
            p1.Priority = Convert.ToInt32(priority2.Text);
            p1.Arrival = Convert.ToInt32(arrival2.Text);
            p1.Name = "P1";
            p1.Color = Objects.Color.white;

            Proc p2 = new Proc();
            p2.Burst = Convert.ToInt32(burst3.Text);
            p2.Priority = Convert.ToInt32(priority3.Text);
            p2.Arrival = Convert.ToInt32(arrival3.Text);
            p2.Color = Objects.Color.orange;
            p2.Name = "P2";
            Proc p3 = new Proc();
            p3.Burst = Convert.ToInt32(burst4.Text);
            p3.Priority = Convert.ToInt32(priority4.Text);
            p3.Arrival = Convert.ToInt32(arrival4.Text);
            p3.Name = "P3";
            p3.Color = Objects.Color.red;

            Proc p4 = new Proc();
            p4.Burst = Convert.ToInt32(burst5.Text);
            p4.Priority = Convert.ToInt32(priority5.Text);
            p4.Arrival = Convert.ToInt32(arrival5.Text);
            p4.Name = "P4";
            p4.Color = Objects.Color.purple;

            Proc p5 = new Proc();
            p5.Burst = Convert.ToInt32(burst6.Text);
            p5.Priority = Convert.ToInt32(priority6.Text);
            p5.Arrival = Convert.ToInt32(arrival6.Text);
            p5.Name = "P5";
            p5.Color = Objects.Color.gray;

            List<Proc> procList = new List<Proc>();
            procList.Add(p0);
            procList.Add(p1);
            procList.Add(p2);
            procList.Add(p3);
            procList.Add(p4);
            procList.Add(p5);

            */
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
            else
            {
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
