using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTcpServer
{
    /// <summary>
    /// Реализация однопоточного сервера.
    /// </summary>
    public class Server
    {
        private int _port;

        /// <summary>
        /// Создание экземпляра сервера.
        /// </summary>
        /// <param name="port">Целевой порт развёртки.</param>
        public Server(int port)
        {
            _port = port;
        }

        /// <summary>
        /// Метод, инциализирующий сокет на переданном порту.
        /// </summary>
        public void EstablishConnection()
        {
            IPEndPoint ipPoint = new(IPAddress.Parse("127.0.0.1"), _port);

            Socket listenSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listenSocket.Bind(ipPoint);

                listenSocket.Listen(10);

                Console.WriteLine("The server is running. Waiting for connections...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new();

                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                    string message = "Your message has been delivered.";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
