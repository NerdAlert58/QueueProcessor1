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
        public MainWindow()
        {
            InitializeComponent();

            var processes = new List<Proc>()
            {
                new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 40, Burst = 15, Arrival = 0, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 30, Burst = 25, Arrival = 25, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 30, Burst = 20, Arrival = 30, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 35, Burst = 15, Arrival = 50, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 5, Burst = 15, Arrival = 100, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P6", Color = Objects.Color.orange, Priority = 10, Burst = 10, Arrival = 105, TurnAroundTime = -1, WaitTime = -1}
            };

            var handler = new Handler(processes);

            var (events, results) = handler.DoWork();

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
        {
            Proc p0 = new Proc();
            p0.Burst = Convert.ToInt32(burst1.Text);
            p0.Priority = Convert.ToInt32(priority1.Text);
            p0.Arrival = Convert.ToInt32(arrival1.Text);
            p0.Name = "P0";

            Proc p1 = new Proc();
            p1.Burst = Convert.ToInt32(burst2.Text);
            p1.Priority = Convert.ToInt32(priority2.Text);
            p1.Arrival = Convert.ToInt32(arrival2.Text);
            p1.Name = "P1";

            Proc p2 = new Proc();
            p2.Burst = Convert.ToInt32(burst3.Text);
            p2.Priority = Convert.ToInt32(priority3.Text);
            p2.Arrival = Convert.ToInt32(arrival3.Text);
            p2.Name = "P2";

            Proc p3 = new Proc();
            p3.Burst = Convert.ToInt32(burst4.Text);
            p3.Priority = Convert.ToInt32(priority4.Text);
            p3.Arrival = Convert.ToInt32(arrival4.Text);
            p3.Name = "P3";

            Proc p4 = new Proc();
            p4.Burst = Convert.ToInt32(burst5.Text);
            p4.Priority = Convert.ToInt32(priority5.Text);
            p4.Arrival = Convert.ToInt32(arrival5.Text);
            p4.Name = "P4";

            Proc p5 = new Proc();
            p5.Burst = Convert.ToInt32(burst6.Text);
            p5.Priority = Convert.ToInt32(priority6.Text);
            p5.Arrival = Convert.ToInt32(arrival6.Text);
            p5.Name = "P5";

            List<Proc> procList = new List<Proc>();
            procList.Add(p0);
            procList.Add(p1);
            procList.Add(p2);
            procList.Add(p3);
            procList.Add(p4);
            procList.Add(p5);

            //scott, call your function here
        }
    }
}
