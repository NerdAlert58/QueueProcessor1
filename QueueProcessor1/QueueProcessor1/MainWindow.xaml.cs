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
        public MainWindow()
        {
            InitializeComponent();

            var processes = new List<Proc>()
            {
                new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 40, Burst = 15, Arrival = 0},
                new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 30, Burst = 25, Arrival = 25},
                new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 30, Burst = 20, Arrival = 30},
                new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 35, Burst = 15, Arrival = 50},
                new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 5, Burst = 15, Arrival = 100},
                new Proc() { Name = "P6", Color = Objects.Color.orange, Priority = 10, Burst = 10, Arrival = 105}
            };

            var handler = new Handler(processes);

            var events = handler.DoWork();

            for (int j = 0; j < 15; j++)
            {
                Button MyControl1 = new Button();
                MyControl1.Content =j.ToString();
                MyControl1.Name = "Button" + j.ToString();
                MyControl1.SetValue(Grid.ColumnProperty, j);
                MyControl1.Height = 35;
                queuegrid.ColumnDefinitions.Add(new ColumnDefinition());
                queuegrid.Children.Add(MyControl1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
