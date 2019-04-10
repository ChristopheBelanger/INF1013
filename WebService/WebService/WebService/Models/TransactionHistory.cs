using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class TransactionHistory : IComparable
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string Wallet { get; set; }
        public double value { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }

        public TransactionHistory(long v1, string action, string wallet, double v2, string date)
        {
            Id = v1;
            Action = action;
            Wallet = wallet;
            value = v2;
            Date = date;
            Status = "Completed";
        }
        public TransactionHistory(long v1, string action, string wallet, double v2, string date, string status)
        {
            Id = v1;
            Action = action;
            Wallet = wallet;
            value = v2;
            Date = date;
            Status = status;
        }
        public int CompareTo(object obj)
        {
            return Id.CompareTo(((TransactionHistory)obj).Id);
        }
    }
}
