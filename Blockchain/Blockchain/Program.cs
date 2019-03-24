﻿using System;
using Newtonsoft.Json;

namespace Blockchain
{
    class MainClass
    {
        public static string Port { get; set; }
        public static Blockchain blockchain { get; set; }

        public static void Main(string[] args)
        {
            //var startTime = DateTime.Now;


            //Blockchain phillyCoin = new Blockchain();
            //phillyCoin.CreateTransaction(new Transaction("Henry", "MaHesh", 10));
            //phillyCoin.ProcessPendingTransactions("Bill");
            ////Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));

            //phillyCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            //phillyCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            //phillyCoin.ProcessPendingTransactions("Bill");

            //var endTime = DateTime.Now;

            //Console.WriteLine($"Duration: {endTime - startTime}");

            //Console.WriteLine("=========================");
            ////Console.WriteLine($"Henry' balance: {phillyCoin.GetBalance("Henry")}");
            ////Console.WriteLine($"MaHesh' balance: {phillyCoin.GetBalance("MaHesh")}");
            ////Console.WriteLine($"Bill' balance: {phillyCoin.GetBalance("Bill")}");

            //Console.WriteLine("=========================");
            //Console.WriteLine($"phillyCoin");
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));

            Console.ReadKey();
            blockchain = new Blockchain();
            if (Array.Find(args, s => s.Equals("-s"))!=null)
            {
                NodeServer nodeServer = new NodeServer(blockchain);
                nodeServer.Start();
            }else if(Array.Find(args, s => s.Equals("-c")) != null)
            {
                NodeClient nodeClient = new NodeClient(blockchain);
                nodeClient.Start();
            }





        }
    }
}
