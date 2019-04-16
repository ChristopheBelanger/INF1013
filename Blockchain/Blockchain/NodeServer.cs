using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;

namespace Blockchain
{
    public class NodeServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        String Ip  { get; set; } = "127.0.0.1";
        int Port { get; set; } = 8080;
        public Blockchain Blockchain { get; set; }
        public Block NewBlock { get; set; } = null;

        public NodeServer(Blockchain blockchain, String ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
            this.Blockchain = blockchain;
        }
        public NodeServer(Blockchain blockchain, int port)
        {
            this.Port = port;
            this.Blockchain = blockchain;
        }
        public NodeServer(Blockchain blockchain)
        {
            this.Blockchain = blockchain;
        }

        public void Start()
        {
            this.tcpListener = new TcpListener(IPAddress.Parse(Ip), Port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();
            Console.WriteLine("Server has started on {0}:{1}.{2}En Attente de connexion...", Ip, Port, Environment.NewLine);

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();
                Console.WriteLine("Un Client s'est connecte.");

                // here was first an message that send hello client
                NetworkStream clientStream = client.GetStream();
                String hello = String.Format("Succes de connexion a {0}:{1}", Ip, Port);
                byte[] bytes = Encoding.ASCII.GetBytes(hello);
                clientStream.Write(bytes, 0, bytes.Length);


                //create a thread to handle communication
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));

                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            while (true)
            {
                byte[] message = new byte[4096];
                int bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, message.Length);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                String bufferincmessage = encoder.GetString(message, 0, bytesRead);


                if (Regex.IsMatch(bufferincmessage, "getBlockChain", RegexOptions.IgnoreCase))
                {
                    String bc= JsonConvert.SerializeObject(Blockchain);
                    byte[] bytes = Encoding.ASCII.GetBytes(bc);
                    clientStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
