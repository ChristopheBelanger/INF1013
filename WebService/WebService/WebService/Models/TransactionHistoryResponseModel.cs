using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class TransactionHistoryResponseModel
    {
        public List<TransactionHistory> Transactions { get; set; }

        public TransactionHistoryResponseModel(List<TransactionHistory> tx) {
            Transactions = tx;
        }
    }
}
