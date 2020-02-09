using Monitoring.Models;
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
using Monitoring.Properties;
using System.IO;
using System.Windows.Forms;

using Monitoring.Properties;

namespace Monitoring.Tabs
{
    public partial class SettingsTab : System.Windows.Controls.UserControl
    {
        public SettingsTab()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientCopmiler cc = new ClientCopmiler(txt_IpClient.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.savePath = txt_SavePath.Text;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                Settings.Default.savePath = folderBrowser.SelectedPath;
                txt_SavePath.Text = folderBrowser.SelectedPath;
            }
        }
    }
}
