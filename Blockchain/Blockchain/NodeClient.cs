using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Blockchain
{
    public class NodeClient
    {
        private TcpClient client;
        String ip { get; set; } = "127.0.0.1";
        int port { get; set; } = 8080;
        Blockchain blockchain;

        public NodeClient(String ip, int port, Blockchain blockchain)
        {
            this.ip = ip;
            this.port = port;
            this.blockchain = blockchain;
        }
        public NodeClient(Blockchain blockchain)
        {
            this.blockchain = blockchain;
        }

        public void Start()
        {
            try
            {
                client = new TcpClient();
                Console.WriteLine("Connecting.....");

                client.Connect(ip, port);
                // use the ipaddress as in the server program

                NetworkStream stm = client.GetStream();

                byte[] message = new byte[stm.Length];
                int bytesRead = 0;  
                bytesRead = stm.Read(message, 0, message.Length);

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                String bufferincmessage = encoder.GetString(message, 0, bytesRead);


                client.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }

        }

    }
}
