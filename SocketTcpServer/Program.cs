using System;

namespace SocketTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new(8007);

            server.EstablishConnection();

            Console.ReadKey();
        }
    }
}
