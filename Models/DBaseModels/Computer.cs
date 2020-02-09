using System.IO;
using System.Text;
using System.Windows;

namespace Monitoring.Models.DBaseModels
{
    public class Computer
    {
        public string MessegeFromClient { get; set; }

        public string ComputerName { get; set; }

        public string ComputerAdress { get; set; }

        public string ProcName { get; set; }
        public string ProcWorkLoad { get; set; }
        public string ProcStatus { get; set; }
        public string ProcSocket { get; set; }
        public string ProcState { get; set; }
        public string ProcMaxRate { get; set; }
        public string ProcPhysicalCore { get; set; }
        public string ProcLogicalCore { get; set; }

        public string VideocardName  { get; set; }
        public long VideocardCountSize  { get; set; }
        public string VideocardStatus  { get; set; }
        public string RefreashRate { get; set; }
        public string VideodriverVersion  { get; set; }
        public string VidiocardStatus  { get; set; }
        public string Vidioprocessor { get; set; }

        public string Bios { get; set; }
        public string BiosCompany { get; set; }

        public long RAMCount { get; set; }


        public Computer(string message)
        {
            Stream s = new MemoryStream(Encoding.Default.GetBytes(message));
            MessageBox.Show(message);
            StreamReader sr = new StreamReader(s);

            MessegeFromClient = message;

            ComputerAdress = sr.ReadLine();

            ComputerName = sr.ReadLine();
            ProcName = sr.ReadLine(); 
            ProcWorkLoad = sr.ReadLine();
            ProcStatus = sr.ReadLine();
            ProcSocket = sr.ReadLine();
            ProcState = sr.ReadLine();
            ProcMaxRate = sr.ReadLine();
            ProcPhysicalCore = sr.ReadLine();
            ProcLogicalCore = sr.ReadLine();

            VideocardName = sr.ReadLine();

            VideocardCountSize = long.Parse(sr.ReadLine());
            VideocardStatus = sr.ReadLine();
            RefreashRate = sr.ReadLine();
            VideodriverVersion = sr.ReadLine();
            VidiocardStatus = sr.ReadLine();
            Vidioprocessor = sr.ReadLine();

            Bios = sr.ReadLine();
            BiosCompany = sr.ReadLine();
            RAMCount = long.Parse(sr.ReadLine()) / 1048576;

        }
    }

}


