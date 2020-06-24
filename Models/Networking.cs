using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Monitoring.Models
{
    delegate void ClientConnectHendler(string message);

    class Networking
    {
        public IPAddress ipAddr { get; }
        public IPEndPoint ipEndPoint { get; }
        public TcpListener server;
        public Thread listen;
        TcpClient client;


        public event ClientConnectHendler Conected;

        public Networking(string IpAddr, int port)
        {
            ipAddr = IPAddress.Parse(IpAddr);
            ipEndPoint = new IPEndPoint(ipAddr, port);
        }

        public void Start(bool isStarted)
        {
            if (isStarted == false)
            {
                server = new TcpListener(ipEndPoint);
                server.Start();
                listen = new Thread(ClientConnect);
                listen.Start();
            }
            else
            {
                server.Server.Close();
                listen.Abort();
            }
        }

        public void ClientConnect()
        {
            while (true)
            {
                client = server.AcceptTcpClient();

                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                IPEndPoint ipep = (IPEndPoint)client.Client.RemoteEndPoint;
                IPAddress ipa = ipep.Address;

                response.AppendLine(ipa.ToString());
                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                if (Conected != null) Conected(response.ToString());

            }
        }
    }
}
