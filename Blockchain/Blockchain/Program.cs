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
            blockchain = new Blockchain();
            AddresseIP addresseService = new AddresseIP();
            addresseService.ip = "dreamtv.dlinkddns.com";//69.51.252.162;
            addresseService.port = 5000;
            AddresseIP addresse = new AddresseIP();
            addresse.ip = "127.0.0.1";
            try
            {
                addresse.port = int.Parse(args[0]);
            }
            catch
            {
                addresse.port = 9005;
            }

            Node node = new Node(addresseService, addresse);
        }
    }
}
