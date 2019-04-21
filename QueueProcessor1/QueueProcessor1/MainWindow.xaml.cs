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
