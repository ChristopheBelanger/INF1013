using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public double Content { get; set; }

        public string Date { get; set; }

        public Transaction()
        {
            var dt = DateTime.Now;
            Date = dt.ToShortDateString() + " " + dt.ToLongTimeString();
        }

        public Transaction(long i, string from, string to, double content)
        {
            Id = i;
            FromWallet = from;
            ToWallet = to;
            Content = content;
            var dt = DateTime.Now;
            Date = dt.ToShortDateString() + " " + dt.ToLongTimeString();
        }



        public Transaction(long i, string from, string to, double content, string date)
        {
            Id = i;
            FromWallet = from;
            ToWallet = to;
            Content = content;
            Date = date;

        }

        public TransactionHistory ToTransactionHistory(string concernedWallet, string status)
        {
            var action = ToWallet == concernedWallet ? "Send To" : "From";
            return new TransactionHistory(Id, action, concernedWallet, Content, Date, status);
        }
    }
}
