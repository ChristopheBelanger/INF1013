using System;
namespace Blockchain
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Montant { get; set; }

        public Transaction(string fromAddress, string toAddress, int montant)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Montant = montant;
        }
    }
}
