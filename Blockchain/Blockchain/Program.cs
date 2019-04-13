using System;
using Newtonsoft.Json;

namespace Blockchain
{
    class MainClass
    {
        public static string Port { get; set; }
        public static Blockchain blockchain { get; set; }

        public static void Main(string[] args)
        {

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
