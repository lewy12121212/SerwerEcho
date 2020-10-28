using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Lab1lib;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Serwer a = new Serwer();
            a.connect();

        }
    }
}
