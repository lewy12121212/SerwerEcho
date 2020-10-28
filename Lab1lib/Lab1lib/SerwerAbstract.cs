using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Lab1lib
{
    public abstract class SerwerAbstract
    {
        #region Pola
        IPAddress IPAddress;
        int Port;
        //int buffer_size = 1024;
        //bool running;
        TcpListener TcpList;
        TcpClient TcpCli;
        NetworkStream stream;
        #endregion

        #region metody
        public SerwerAbstract(IPAddress IP, int port)
        {
            this.IPAddress = IP;
            this.Port = port;
        }

        protected void StartListening()
        {
            TcpList = new TcpListener(IPAddress, Port);
            TcpList.Start();
        }

        protected TcpListener TcpListener { get => TcpList; set => TcpList = value; }
        protected TcpClient TcpClient { get => TcpCli; set => TcpCli = value; }
        protected NetworkStream Stream { get => stream; set => stream = value; }

        public abstract void Start();
        public abstract void AcceptClient();
        protected abstract void BeginDataTransmission(NetworkStream nwStream, int client_buf_size);
        #endregion
    }
}
