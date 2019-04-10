using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Database;
using WebService.Models;

namespace WebService.Containers
{
    public static class TransactionPool
    {
        private static object TransactionLock = new object();
        static int NextTxId = 0;
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

        public static void FinishTx(int[] id)
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
                if (TxPool.Count > 0)
                {
                    var nbFetch = TxPool.Count >= 4 ? 4 : TxPool.Count;
                    fetchedTx = TxPool.GetRange(0, nbFetch);
                    PendingTx.AddRange(fetchedTx);
                    TxPool.RemoveRange(0, nbFetch);
                }
            }
            return fetchedTx;
        }

        private static void SaveTransaction(List<Transaction> finishedTransactions)
        {
            var insertStatement = "INSERT INTO TRANSACTION (FromWallet,ToWallet,Content) Values";
            var baseInsertValues = " (?fromWallet,?toWallet,?content),";
            foreach (Transaction t in finishedTransactions) {
                var insertRow = baseInsertValues.Replace("?fromWallet", "'" + t.FromWallet + "'");
                insertRow = insertRow.Replace("?toWallet", "'" + t.ToWallet + "'");
                insertRow = insertRow.Replace("?content", t.Content.ToString());
                insertStatement += insertRow;
            }
            insertStatement = insertStatement.Remove(insertStatement.Length - 1);
            DatabaseHelper.GetInstance().ExecuteSQL(insertStatement);
        }
    }
}
