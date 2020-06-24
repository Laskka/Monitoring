using Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Monitoring.Properties;
using System.Windows.Forms;

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
            Settings.Default.savePath = txt_SavePath.Text;
            Settings.Default.Save();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                Settings.Default.savePath = folderBrowser.SelectedPath;
                Settings.Default.Save();
                txt_SavePath.Text = folderBrowser.SelectedPath;
            }
        }
    }
}
