using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Containers
{
    public static class TransactionPool
    {
        private static object TransactionLock = new object();
        static long NextTxId = 0;
        static List<Transaction> TxPool = new List<Transaction>();
        static List<Transaction> PendingTx = new List<Transaction>();

        public static void addTx(Transaction tx)
        {
            lock (TransactionLock)
            {
                tx.Id = NextTxId++;
                TxPool.Add(tx);
            }
        }

        public static void FinishTx(long[] id)
        {
            var completedTx = new List<Transaction>();
            lock (TransactionLock)
            {
                foreach (long i in id)
                {
                    var tx = PendingTx.Find(pendingTx => pendingTx.Id == i);
                    if (tx != null)
                    {
                        completedTx.Add(tx);
                        PendingTx.Remove(tx);
                    }
                }
                ClearPendingTx();
            }
            SaveTransaction(completedTx);

        }

        private static void ClearPendingTx()
        {
            lock (TransactionLock)
            {
                PendingTx = new List<Transaction>();
            }
        }

        public static List<Transaction> FetchTransactions()
        {
            List<Transaction> fetchedTx = new List<Transaction>();
            lock (TransactionLock)
            {
                fetchedTx = TxPool.GetRange(0, 4);
                PendingTx.AddRange(fetchedTx);
                TxPool.RemoveRange(0, 4);
            }
            return fetchedTx;
        }

        private static void SaveTransaction(List<Transaction> finishedTransactions)
        {
        }
    }
}
