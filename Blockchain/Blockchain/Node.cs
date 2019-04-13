using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Blockchain
{
    public class Node
    {
        IList<Transaction> pendingTransactions;
        IList<String> addressesNoeuds;
        NodeServer serveur;
        TcpClient clientService;
        NetworkStream clientServiceStream;
        Blockchain blockchain;
        int rank { get; set; } = 0;

        public Node(AddresseIP addresseService)
        {
            pendingTransactions = new List<Transaction>();
            addressesNoeuds = new List<String>();

            // connexion au service, obtenir liste des noeuds maitres et des transactions non traitees
            clientService = new TcpClient();
            clientService.Connect(addresseService.ip, addresseService.port);
            clientServiceStream = clientService.GetStream();
            // attendre reponse du service
            byte[] message = new byte[4096];
            int bytesRead = 0;

            try
            {
                //blocks until a client sends a message
                bytesRead = clientServiceStream.Read(message, 0, message.Length);
            }
            catch (Exception e)
            {
                //a socket error has occured
                Console.Write(e.Message);
            }

            if (bytesRead == 0)
            {
                //the client has disconnected from the server
                Console.Write("Client deconnecte..");
                throw new Exception("Nothing to read from stream..");
            }
            //message has successfully been received
            ASCIIEncoding encoder = new ASCIIEncoding();

            String bufferincmessage = encoder.GetString(message, 0, bytesRead);


            Run();
        }

        public Node(AddresseIP addresseService, AddresseIP addresseNoeud)
        {
            untreatedTransactions = new List<Transaction>();
            pendingTransactions = new List<Transaction>();
            clientService = new TcpClient(addresseService.ip, addresseService.port);
            serveur = new NodeServer(blockchain, addresseNoeud.ip, addresseNoeud.port);
            Run();
        }

        public void Run()
        {
            serveur = new NodeServer(blockchain);
        }
    }
}
