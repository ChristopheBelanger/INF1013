using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class TransactionHistory
    {
        int Id;
        string Action;
        string Wallet;
        double value;

        public TransactionHistory(int v1, string action, string wallet, double v2)
        {
            Id = v1;
            Action = action;
            Wallet = wallet;
            value = v2;
        }
    }
}
