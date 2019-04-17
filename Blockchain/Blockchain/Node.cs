using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain
{
    public class Node
    {
        List<Transaction> pendingTransactions;
        List<String> addressesNoeuds;
        NodeServer nodeServer;
        NodeClient nodeClient;
        WebClient serviceClient;
        NetworkStream clientServiceStream;
        Blockchain Blockchain { get; }
        Block treatedBlock = null;
        int rank { get; set; } = 0;
        DateTime lastRequest;
        Boolean master = false;
        Boolean pending = false;
        AddresseIP nodeAddress { get; set; }
        AddresseIP serviceAddress { get; set; }
        AddresseIP masterIP { get; set; } = new AddresseIP();

        public Node(AddresseIP addresseService, AddresseIP addresse)
        {
            pendingTransactions = new List<Transaction>();
            addressesNoeuds = new List<String>();
            this.nodeAddress = addresse;
            serviceAddress = addresseService;

            // connexion au service, obtenir liste des noeuds maitres et des transactions non traitees
            String addr = "'" + addresse.ip + ":" + addresse.port + "'";
            serviceClient = new WebClient();
            serviceClient.Headers.Add("Content-Type", "application/json");
            String responseString = serviceClient.UploadString("https://" + serviceAddress.ip + ":5001/api/Connection", addr);

            responseString = responseString.Replace("[", "").Replace("]", "").Replace("\"", "");
            String[] temp = responseString.Split(',');
            foreach (String s in temp)
            {
                if (!s.Equals(""))
                {
                    addressesNoeuds.Add(s);
                }
            }
            addressesNoeuds.Remove(addresse.ip + ":" + addresse.port);

            if (addressesNoeuds.Count == 0)
            {
                master = true;
                Blockchain = new Blockchain();
                nodeServer = new NodeServer(Blockchain, addresse.ip, addresse.port);
                nodeServer.Start();

            }
            else
            {
                rank = addressesNoeuds.Count;
                temp = addressesNoeuds[0].Split(':');
                masterIP.ip = temp[0];
                masterIP.port = int.Parse(temp[1]);
                nodeClient = new NodeClient(null, addresse.ip, addresse.port);
                if (nodeClient.ConnectToMaster(masterIP.ip, masterIP.port))
                {
                    //get blockchain du master
                    Blockchain = nodeClient.GetBlockChain(masterIP.ip, masterIP.port);
                }
                nodeServer = new NodeServer(Blockchain, addresse.ip, addresse.port);
                nodeServer.Start();
            }
            lastRequest = DateTime.Now;
            Run();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    if (master)
                    {
                        if (nodeServer.NewBlock != null && Blockchain.GetLatestBlock().PreviousHash == nodeServer.NewBlock.PreviousHash)
                        {

                            Blockchain.AddBlock(nodeServer.NewBlock);
                            nodeServer.NewBlock = null;
                            pendingTransactions = new List<Transaction>();
                            List<String> ids = new List<String>();
                            foreach (Transaction t in Blockchain.GetLatestBlock().Transactions)
                            {
                                ids.Add(t.Id.ToString());
                            }
                            String idsToSend = JsonConvert.SerializeObject(ids);
                            String responseString = serviceClient.UploadString("https://"+ serviceAddress.ip+":5001/api/Transaction", idsToSend);
                            nodeServer.PropagateBlock(Blockchain.GetLatestBlock());
                            pending = false;
                        }
                    }
                    else
                    {
                        if (nodeClient.NewBlock != null)
                        {
                            Blockchain.AddBlock(nodeClient.NewBlock);
                            nodeClient.NewBlock = null;
                            pendingTransactions = new List<Transaction>();
                            pending = false;
                        }
                    }
                    TimeSpan timeSpan = DateTime.Now.Subtract(lastRequest);
                    if (pendingTransactions.Count <= 0 && !pending && timeSpan.Seconds > 15)
                    {
                        //get transactions from service
                        lastRequest = DateTime.Now;
                        String responseString = serviceClient.DownloadString("https://" + serviceAddress.ip + ":5001/api/Transaction");
                        pendingTransactions = JsonConvert.DeserializeObject<List<Transaction>>(responseString);
                    }
                    if (pendingTransactions.Count > 0 && !pending)
                    {
                        Blockchain.PendingTransactions = pendingTransactions;
                        treatedBlock = Blockchain.ProcessPendingTransactions(nodeAddress.ToString());
                        if (master)
                        {
                            if (Blockchain.GetLatestBlock().PreviousHash == treatedBlock.PreviousHash)
                            {
                                if (nodeServer.NewBlock != null && Blockchain.GetLatestBlock().PreviousHash == nodeServer.NewBlock.PreviousHash)
                                {
                                    if (nodeServer.NewBlock.TimeStamp < treatedBlock.TimeStamp)
                                    {
                                        Blockchain.AddBlock(nodeServer.NewBlock);
                                    }
                                    else
                                    {
                                        Blockchain.AddBlock(treatedBlock);
                                    }
                                    nodeServer.NewBlock = null;
                                    treatedBlock = null;
                                    pendingTransactions = new List<Transaction>();
                                    List<String> ids = new List<String>();
                                    foreach (Transaction t in Blockchain.GetLatestBlock().Transactions)
                                    {
                                        ids.Add(t.Id.ToString());
                                    }
                                    String idsToSend = JsonConvert.SerializeObject(ids);
                                    //serviceClient = new WebClient();
                                    serviceClient.Headers.Add("Content-Type", "application/json");
                                    String responseString = serviceClient.UploadString("https://" + serviceAddress.ip + ":5001/api/Transaction", idsToSend);
                                    nodeServer.PropagateBlock(Blockchain.GetLatestBlock());

                                }
                                else
                                {
                                    Blockchain.AddBlock(treatedBlock);
                                    treatedBlock = null;
                                    pendingTransactions = new List<Transaction>();
                                    List<String> ids = new List<String>();
                                    foreach (Transaction t in Blockchain.GetLatestBlock().Transactions)
                                    {
                                        ids.Add(t.Id.ToString());
                                    }
                                    String idsToSend = "'" + JsonConvert.SerializeObject(ids) + "'";
                                    //serviceClient = new WebClient();
                                    serviceClient.Headers.Add("Content-Type", "application/json");
                                    String responseString = serviceClient.UploadString("https://" + serviceAddress.ip + ":5001/api/Transaction", idsToSend);
                                    nodeServer.PropagateBlock(Blockchain.GetLatestBlock());
                                }
                            }
                        }
                        else
                        {
                            if (Blockchain.GetLatestBlock().PreviousHash == treatedBlock.PreviousHash)
                            {
                                if (nodeClient.NewBlock != null)
                                {
                                    Blockchain.AddBlock(nodeServer.NewBlock);
                                    nodeClient.NewBlock = null;
                                    treatedBlock = null;
                                    pendingTransactions = new List<Transaction>();
                                    pending = false;
                                }
                                else
                                {
                                    nodeClient.PushBlock(treatedBlock);
                                    treatedBlock = null;
                                    pendingTransactions = new List<Transaction>();
                                    pending = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
