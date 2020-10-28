using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Lab1lib
{
    public class Serwer : SerwerAbstract
    {
        int bytetoint(byte[] bufor, int bytes_r)
        {
            string a = ASCIIEncoding.ASCII.GetString(bufor, 0 ,bytes_r);
            int x = 0;
            int pow = 2;
            if (Int32.TryParse(a, out x))
            {
                for(int i = 1; i < x; i++)
                {
                    pow = pow * 2;
                }

                Console.Write("2 to "+ x + "power is: " + pow + "\n");
                return pow;

            } else
            {
                return 0;
            }
            
        }

        public delegate void TransmissionDataDelegate(NetworkStream nwStream, int client_buf_size);

        public Serwer(IPAddress IP, int port) : base(IP, port) {

        }

        public void sayhello(NetworkStream nwStream)
        {
            byte[] buffer = new byte[512];
            String letter = "Witaj na serwerze - podaj login:  \n\r";
            buffer = Encoding.ASCII.GetBytes(letter);
            nwStream.Write(buffer, 0, buffer.Length);
        }

        public void login(NetworkStream nwStream)
        {
            byte[] buffer = new byte[512];
            String letter = "Zalogowano poprawnie :)";
            buffer = Encoding.ASCII.GetBytes(letter);
            nwStream.Write(buffer, 0, buffer.Length);
        }

        public void loginerror(NetworkStream nwStream)
        {
            byte[] buffer = new byte[512];
            String letter = "Problem z zalogowaniem: spróbuj raz jeszcze: ";
            buffer = Encoding.ASCII.GetBytes(letter);
            nwStream.Write(buffer, 0, buffer.Length);
        }

        public override void AcceptClient()
        {          
            while (true)
            {
                TcpClient client = TcpListener.AcceptTcpClient();


                //NetworkStream nwStream = client.GetStream();
                Stream = client.GetStream();

                sayhello(Stream);
                int client_buf_size = client.ReceiveBufferSize;
                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(Stream, client_buf_size ,TransmissionCallback, client);
                

            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
            Console.Write("koniec sesji");
        }

        protected override void BeginDataTransmission(NetworkStream nwStream, int client_buf_size)
        {
            byte[] buffer = new byte[client_buf_size];
            bool if_login = false;

            while (true)
            {
                try
                {
                    int bytesRead = nwStream.Read(buffer, 0, client_buf_size);
                    string str = Encoding.ASCII.GetString(buffer);

                    if(str.Contains(":q!"))
                    {
                        break;
                    } else if(str.Contains("admin|admin") && if_login == false)
                    {
                        login(nwStream);
                        if_login = true;
                    } else if(if_login == false)
                    {
                        loginerror(nwStream);
                    } else if(if_login == true)
                    {
                        nwStream.Write(buffer, 0, bytesRead);
                    }

                    //nwStream.Write(buffer, 0, bytesRead);
                }
                catch (IOException e)
                {
                    break;
                }
            }

        }


        public override void Start()
        {
            StartListening();
            AcceptClient();
        }

    }
}

