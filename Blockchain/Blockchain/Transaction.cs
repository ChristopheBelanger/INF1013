using System;
namespace Blockchain
{
    public class Transaction
    {
        public int Id { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }

        public Transaction(int id, string fromAddress, string toAddress, int amount)
        {
            Id = id;
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }

        public override string ToString()
        {
            return "ID=" + Id + ";From=" + FromAddress + ";To=" + ToAddress + ";Amount=" + Amount;
        }
    }
}
