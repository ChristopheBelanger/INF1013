using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class TransactionHistory
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string Wallet { get; set; }
        public double value { get; set; }

        public TransactionHistory(long v1, string action, string wallet, double v2)
        {
            Id = v1;
            Action = action;
            Wallet = wallet;
            value = v2;
        }
    }
}
