using Monitoring.Models.DBaseModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Monitoring.Tabs
{
    public delegate void PressToShowComputer(Computer message);

    public partial class ComputerTile : UserControl
    {
        public event PressToShowComputer pressed;

        Computer computer;

        public ComputerTile(Computer comp)
        {
            InitializeComponent();
            computer = comp;
            Margin = new Thickness(20);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pressed != null) pressed(computer);
        }

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream($"{Properties.Settings.Default.savePath}/{computer.ComputerName}1.txt", FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(computer.MessegeFromClient);
                }
            }
        }
    }
}
