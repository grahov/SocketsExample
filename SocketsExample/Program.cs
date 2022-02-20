using System;

namespace SocketsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketTcpClientLibrary.Client client = new(8007);

            client.Connect();

            Console.ReadKey();
        }
    }
}
