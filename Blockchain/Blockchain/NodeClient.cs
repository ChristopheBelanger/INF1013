using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;

namespace Blockchain
{
    public class NodeClient
    {
        private TcpClient client;
        String Ip { get; set; } = "127.0.0.1";
        int Port { get; set; } = 8080;
        public Blockchain Blockchain { get; set; }
        public Block NewBlock { get; set; } = null;
        NetworkStream stream;

        public NodeClient(Blockchain blockchain, String ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
            this.Blockchain = blockchain;
        }
        public NodeClient(Blockchain blockchain)
        {
            this.Blockchain = blockchain;
        }

        public bool ConnectToMaster(String masterIP, int masterPort)
        {
            try
            {
                client = new TcpClient();
                Console.WriteLine("Connexion.....");

                client.Connect(masterIP, masterPort);
                // use the ip address as in the server program

                stream = client.GetStream();

                byte[] message = new byte[4096];
                int bytesRead = 0;
                bytesRead = stream.Read(message, 0, message.Length);

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                String bufferincmessage = encoder.GetString(message, 0, bytesRead);
                if (bufferincmessage.Contains("Succes de connexion"))
                {
                    //create a thread to handle communication
                    //with connected client
                    Thread serverThread = new Thread(new ParameterizedThreadStart(HandleServerComm));

                    serverThread.Start(client);
                    return true;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Erreur..... " + e.StackTrace);
            }
            return false;
        }
        public void CloseMasterConnection()
        {
            client.Close();
        }

        public Blockchain GetBlockChain()
        {
            try
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("getBlockChain");


                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", "getBlockChain");

                // Receive the TcpServer.response.
                byte[] message = new byte[4096];
                int bytesRead = 0;

                try
                {
                    bytesRead = stream.Read(message, 0, message.Length);
                }
                catch (Exception e)
                {
                    //a socket error has occured
                    Console.Write(e.Message);
                }

                if (bytesRead == 0)
                {
                    //the server has disconnected from the server
                    Console.Write("Client deconnecte..");
                    throw new Exception("Nothing to read from stream..");
                }
                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                String bufferincmessage = encoder.GetString(message, 0, bytesRead);

                // Close everything.
                stream.Close();
                //client.Close();

                return JsonConvert.DeserializeObject<Blockchain>(bufferincmessage);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return null;
        }

        private void HandleServerComm(object client)
        {
            while (true)
            {
                byte[] message = new byte[4096];
                int bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = stream.Read(message, 0, message.Length);
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


                if (Regex.IsMatch(bufferincmessage, "pending", RegexOptions.IgnoreCase))
                {
                   //do something..
                }
                else
                {
                    if (Regex.IsMatch(bufferincmessage, "newBlock", RegexOptions.IgnoreCase))
                    {
                        NewBlock = JsonConvert.DeserializeObject<Block>(bufferincmessage.Split('^')[1]);
                    }
                }

            }
        }
    }
}
