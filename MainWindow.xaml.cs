using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

            //создание вкладок в коде, чтобы было удобние к ним обращаться
            act = new AllComutersTab();
            st = new SettingsTab();
            sct = new SelectedComputerTab();

            //переменнная в которой хранится путь сохранения текстовых файлов с данными о клиенских ПК
            st.txt_SavePath.Text = Settings.Default.savePath;

            // добавление вкладок в список на главный экран
            sp_Main.Children.Add(act);
            sp_Main.Children.Add(sct);
            sp_Main.Children.Add(st);

            //создание отдального файла для структурирования кода, да
            mvc = new MainViewController(this);


        }

        //закрытие окна 
        private void Pwr_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        //перемешение плашки под главными кнопкми
        private void mainBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;

            switch (btn.Uid)
            {
                case "1":
                    floatStick.Margin = new Thickness(20 + (353 * 0), 40, 0, 0);
                    act.Visibility = Visibility.Visible;
                    sct.Visibility = Visibility.Hidden;
                    st.Visibility = Visibility.Hidden;
                    break;
                case "2":
                    floatStick.Margin = new Thickness(20 + 353 * 1, 40, 0, 0);
                    act.Visibility = Visibility.Hidden;
                    sct.Visibility = Visibility.Visible;
                    st.Visibility = Visibility.Hidden;
                    break;
                case "3":
                    floatStick.Margin = new Thickness(20 + 353 * 2, 40, 0, 0);
                    act.Visibility = Visibility.Hidden;
                    sct.Visibility = Visibility.Hidden;
                    st.Visibility = Visibility.Visible;
                    break;
            }
        }

        
        private void btn_startServer_Click(object sender, RoutedEventArgs e)
        {
            if (networking == null)
            {
                networking = new Networking(cbx_ipServer.SelectionBoxItem.ToString(), 11000);
                networking.Conected += mvc.ConnectingComputer;
            }

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

        private void btn_Test_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Net.Dns.GetHostName());
            MessageBox.Show(networking.listen.IsAlive.ToString());
            MessageBox.Show(networking.server.Server.Connected.ToString());
            MessageBox.Show(networking.ipAddr.ToString());
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
