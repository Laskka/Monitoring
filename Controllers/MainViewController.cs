using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Monitoring.Models;
using Monitoring.Tabs;
using Monitoring.Properties;
using Monitoring.Models.DBaseModels;
using System.Windows.Threading;
using System.Threading;

using System.DirectoryServices;
using System.Net;
using System.IO;

namespace Monitoring.Controllers
{
    class MainViewController
    {
        Computer selectComputer;

        MainWindow mainWindow;

        List<Computer> computers = new List<Computer>();

        public MainViewController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            SetComboBoxId();

            StartSetView();
        }

        void StartSetView()
        {
            try
            {
                string[] allFoundFiles = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}\\data", "*.txt", SearchOption.TopDirectoryOnly);
                
                if (allFoundFiles.Length != 0)
                {
                    foreach (var item in allFoundFiles)
                    {
                        computers.Add(new Computer(new StreamReader(item).ReadToEnd()));
                    }

                    mainWindow.act.sp_act.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate ()
                    {
                        foreach (var computer in computers)
                        {
                            ComputerTile tile = new ComputerTile(computer);
                            tile.pressed += SelectedComuterTile;
                            tile.text_NameComputer.Content = computer.ComputerName;
                            mainWindow.act.sp_act.Children.Add(tile);
                        }
                    });
                }
            }
            catch (Exception)
            {
            }
        }
         
        void SetComboBoxId()
        {
            foreach (var item in Dns.GetHostByName(Dns.GetHostName()).AddressList)
            {
                mainWindow.cbx_ipServer.Items.Add(item.ToString());
            }

            mainWindow.cbx_ipServer.SelectedIndex = 0;
        }

        public void ConnectingComputer(string str)
        {
            Computer ccc = new Computer(str);

            bool isInComputerList = false;

            for (int i = 0; i < computers.Count; i++)
            {
                if (computers[i].ComputerName == ccc.ComputerName)
                {
                    isInComputerList = true;
                    /*computers[i] = ccc;
                    if (selectComputer.ComputerName == computers[i]?.ComputerName)
                    {
                        SelectedComuterTile(computers[i]);
                    }*/
                    break;
                }
            }
            if (!isInComputerList)
            {
                mainWindow.act.sp_act.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    computers.Add(ccc);
                    var c = new ComputerTile(ccc);
                    c.pressed += SelectedComuterTile;
                    c.text_NameComputer.Content = ccc.ComputerName;
                    mainWindow.act.sp_act.Children.Add(c);
                    
                });
            }
            
            FileStream fs = new FileStream($"{ AppDomain.CurrentDomain.BaseDirectory }\\data\\{ccc.ComputerName}.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(ccc.MessegeFromClient);
            sw.Close();
            fs.Close();
        }

        void SelectedComuterTile(Computer c)
        {
            mainWindow.sct = new SelectedComputerTab();
            mainWindow.sp_Main.Children.Add(mainWindow.sct);
            selectComputer = c;

            mainWindow.floatStick.Margin = new Thickness(20 + 353 * 1, 35, 0, 0);
            mainWindow.act.Visibility = Visibility.Hidden;
            mainWindow.sct.Visibility = Visibility.Visible;
            mainWindow.st.Visibility = Visibility.Hidden;
            
            mainWindow.sct.lbl_gpuName.Text = c.VideocardName;
            mainWindow.sct.lbl_CountRAM.Text = (c.RAMCount).ToString() + "MB";
            mainWindow.sct.lbl_CurrnetComputer.Text = c.ComputerName + "\n" + c.ComputerAdress;
            mainWindow.sct.Cpu_Name.Text = c.ProcName;

            mainWindow.sct.ProcName.Content += " " + c.ProcName;
            mainWindow.sct.ProcWorkLoad.Content += " " + c.ProcWorkLoad;
            mainWindow.sct.ProcSocket.Content += " " + c.ProcSocket;
            mainWindow.sct.ProcMaxRate.Content += " " + c.ProcMaxRate;
            mainWindow.sct.ProcPhysicalCore.Content += " " + c.ProcPhysicalCore;
            mainWindow.sct.ProcLogicalCore.Content += " " + c.ProcLogicalCore;

            mainWindow.sct.VideocardName.Content += " " + c.VideocardName;
            mainWindow.sct.VideocardStatus.Content += " " + c.VideocardStatus;
            mainWindow.sct.VideocardCountSize.Content += " " + c.VideocardCountSize;
            mainWindow.sct.VideodriverVersion.Content += " " + c.VideodriverVersion;
            mainWindow.sct.Vidioprocessor.Content += " " + c.Vidioprocessor;

        }
    }
}
