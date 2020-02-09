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
using Monitoring.Controllers;
using Monitoring.Models;
using Monitoring.Properties;
using Monitoring.Tabs;

namespace Monitoring
{

    public partial class MainWindow : Window
    {
        public AllComutersTab act;
        public SettingsTab st;
        public SelectedComputerTab sct;

        MainViewController mvc;

        Networking networking;

        bool isStarted = false;

        public MainWindow()
        {
            InitializeComponent();

            act = new AllComutersTab();
            st = new SettingsTab();
            sct = new SelectedComputerTab();

            st.txt_SavePath.Text = Settings.Default.savePath;

            sp_Main.Children.Add(act);
            sp_Main.Children.Add(sct);
            sp_Main.Children.Add(st);

            mvc = new MainViewController(this);

            networking = new Networking(cbx_ipServer.SelectionBoxItem.ToString(), 11000);
        }

        private void Pwr_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void mainBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;

            switch (btn.Uid)
            {
                case "1": floatStick.Margin = new Thickness(50 + 200 * int.Parse(btn.Uid), 0, 0, 0);
                    act.Visibility = Visibility.Visible;
                    sct.Visibility = Visibility.Hidden;
                    st.Visibility = Visibility.Hidden;
                    break;
                case "2": floatStick.Margin = new Thickness(50 + 200 * int.Parse(btn.Uid), 0, 0, 0);
                    act.Visibility = Visibility.Hidden;
                    sct.Visibility = Visibility.Visible;
                    st.Visibility = Visibility.Hidden;
                    break;
                case "3": floatStick.Margin = new Thickness(50 + 200 * int.Parse(btn.Uid), 0, 0, 0);
                    act.Visibility = Visibility.Hidden;
                    sct.Visibility = Visibility.Hidden;
                    st.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btn_startServer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(cbx_ipServer.SelectionBoxItem.ToString());

            networking.Conected += mvc.ConnectingComputer;
            if (!isStarted)
            {
                networking.Start(isStarted);
                isStarted = true;
                btn_startServer.Background = new SolidColorBrush(Colors.ForestGreen);
            }
            else
            {
                networking.Start(isStarted);
                isStarted = false;
                btn_startServer.Background = new SolidColorBrush(Colors.IndianRed);
            }
            



        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
