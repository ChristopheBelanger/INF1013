using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Blockchain
{
    public class ServeurNoeud
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        String ip  { get; set; } = "127.0.0.1";
        int port { get; set; } = 8080;
        Blockchain blockchain;
        bool listen = true;

        public ServeurNoeud(Blockchain blockchain, String ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.blockchain = blockchain;
        }
        public ServeurNoeud(Blockchain blockchain, int port)
        {
            this.port = port;
            this.blockchain = blockchain;
        }
        public ServeurNoeud(Blockchain blockchain)
        {
            this.blockchain = blockchain;
        }

        public void Start()
        {
            this.tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        public void Stop()
        {
            tcpListener.Stop();
            listen = false;
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();
            Console.WriteLine("Server has started on {0}:{1}.{2}En Attente de connexion...", ip, port, Environment.NewLine);

            while (listen)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();
                Console.WriteLine("Un Client s'est connecte.");

                // here was first an message that send hello client
                NetworkStream clientStream = client.GetStream();
                String hello = String.Format("Succes de connexion a {0}:{1}", ip, port);
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

            while (listen)
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


                if (Regex.IsMatch(bufferincmessage, Properties.Settings.test, RegexOptions.IgnoreCase))
                {
                //    bufferincmessageresult = bufferincmessage.Split('^');
                //    String nickname_Cl = bufferincmessageresult[1];
                //    String password_Cl = bufferincmessageresult[2];
                //    //getuserdata_db();
                //    //login();

                //    byte[] buffer = encoder.GetBytes(inlogmessage);

                //    clientStream.Write(buffer, 0, buffer.Length);
                //    clientStream.Flush();
                }


            }
        }
    }
}
