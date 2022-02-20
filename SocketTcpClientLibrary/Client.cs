using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTcpClientLibrary
{
    /// <summary>
    /// Реализация клиента.
    /// </summary>
    public class Client
    {
        private int _port;
        private static string s_address = "127.0.0.1";


        /// <summary>
        /// Создание экземпляра клиента.
        /// </summary>
        /// <param name="port">Целевой порт отправки.</param>
        public Client(int port)
        {
            _port = port;
        }

        /// <summary>
        /// Метод, устанавливающий соединение.
        /// </summary>
        public void Connect()
        {
            try
            {
                IPEndPoint ipPoint = new(IPAddress.Parse(s_address), _port);

                Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(ipPoint);
                Console.Write("Enter your message: ");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                data = new byte[256];
                StringBuilder builder = new();
                int bytes = 0;

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);

                Console.WriteLine("server response: " + builder.ToString());

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
