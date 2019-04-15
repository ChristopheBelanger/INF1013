using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Blockchain
{
    public class Node
    {
        List<Transaction> pendingTransactions;
        List<String> addressesNoeuds;
        NodeServer Server { get; }
        TcpClient TcpClient;
        NetworkStream clientServiceStream;
        Blockchain Blockchain { get; }
        int rank { get; set; } = 0;
        Boolean master = false;
        AddresseIP addresse { get; set; }
        AddresseIP masterIP { get; set; } = new AddresseIP();

        public Node(AddresseIP addresseService, AddresseIP addresse)
        {
            pendingTransactions = new List<Transaction>();
            addressesNoeuds = new List<String>();
            this.addresse = addresse;

            // connexion au service, obtenir liste des noeuds maitres et des transactions non traitees
            String addr = "'" + addresse.ip + ":" + addresse.port + "'";
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            String responseString = client.UploadString("https://localhost:5001/api/Connection", addr);

            responseString = responseString.Replace("[", "").Replace("]", "").Replace("\"", "");
            String[] temp = responseString.Split(',');
            foreach (String s in temp)
            {
                addressesNoeuds.Add(s);
            }
            addressesNoeuds.Remove(addresse.ip + ":" + addresse.port);

            if (addressesNoeuds.Count == 0)
            {
                master = true;
                Blockchain = new Blockchain();
                Server = new NodeServer(Blockchain, addresse.ip, addresse.port);
                Server.Start();
            }
            else
            {
                rank = addressesNoeuds.Count;
                temp = addressesNoeuds[0].Split(':');
                masterIP.ip = temp[0];
                masterIP.port = int.Parse(temp[1]);
                //get blockchain du master
                Connect(masterIP.ip, masterIP.port, "getBlockChain");

            }
            String test = "";
            //// connexion au service, obtenir liste des noeuds maitres et des transactions non traitees
            //clientService = new TcpClient();
            //clientService.Connect(addresseService.ip, addresseService.port);
            //clientServiceStream = clientService.GetStream();
            //// attendre reponse du service
            //byte[] message = new byte[4096];
            //int bytesRead = 0;

            //try
            //{
            //    //blocks until a client sends a message
            //    bytesRead = clientServiceStream.Read(message, 0, message.Length);
            //}
            //catch (Exception e)
            //{
            //    //a socket error has occured
            //    Console.Write(e.Message);
            //}

            //if (bytesRead == 0)
            //{
            //    //the client has disconnected from the server
            //    Console.Write("Client deconnecte..");
            //    throw new Exception("Nothing to read from stream..");
            //}
            ////message has successfully been received
            //ASCIIEncoding encoder = new ASCIIEncoding();

            //String bufferincmessage = encoder.GetString(message, 0, bytesRead);
        }

        static void Connect(String server,int port, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
