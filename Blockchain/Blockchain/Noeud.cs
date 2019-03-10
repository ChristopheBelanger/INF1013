using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Blockchain
{
    public class Noeud
    {
        IList<Transaction> transactionsEnAttente;
        IList<Transaction> transactionsEnTraitement;
        IList<String> addressesNoeuds;
        ServeurNoeud serveur;
        TcpClient clientService;
        Blockchain blockchain;

        public Noeud(AddresseIP addresseService)
        {
            transactionsEnAttente = new List<Transaction>();
            transactionsEnTraitement = new List<Transaction>();
            addressesNoeuds = new List<String>();

            // connexion au service, obtenir liste des autres noeuds et des transactions non traitees
            clientService = new TcpClient();
            clientService.Connect(addresseService.ip, addresseService.port);


            Run();
        }

        public Noeud(AddresseIP addresseService, AddresseIP addresseNoeud)
        {
            transactionsEnAttente = new List<Transaction>();
            transactionsEnTraitement = new List<Transaction>();
            clientService = new TcpClient(addresseService.ip, addresseService.port);
            serveur = new ServeurNoeud(blockchain, addresseNoeud.ip, addresseNoeud.port);
            Run();
        }

        public void Run()
        {
            serveur = new ServeurNoeud(blockchain);
        }
    }
}
