using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Database
{
    public static class DbModelParser
    {
        public static List<Wallet> ParseWallet(MySqlDataReader reader, string id) {
            var wallets = new List<Wallet>();
            while (reader.Read())
            {
                wallets.Add(new Wallet(reader.GetFieldValue<string>(0), (double)reader.GetFieldValue<decimal>(1)));
            }
            return wallets;
        }

        public static List<Transaction> ParseTransaction(MySqlDataReader reader, string id)
        {
            var transactions = new List<Transaction>();   
            while (reader.Read())
            {
                transactions.Add(new Transaction(reader.GetFieldValue<int>(0), reader.GetFieldValue<string>(1), reader.GetFieldValue<string>(2), reader.GetFieldValue<double>(3), reader.GetFieldValue<string>(4)));
            }
            return transactions;
        }

        public static List<TransactionHistory> ParseTransactionHistory(MySqlDataReader reader, string id)
        {
            var transactions = new List<TransactionHistory>();
            while (reader.Read())
            {
                string wallet;
                string action;
                if (id == reader.GetFieldValue<string>(1))
                {
                    action = "Send To";
                    wallet = id;
                }
                else {
                    action = "From";
                    wallet = reader.GetFieldValue<string>(2);
                }
                transactions.Add(new TransactionHistory(reader.GetFieldValue<long>(0), action, wallet, reader.GetFieldValue<double>(3), reader.GetFieldValue<string>(4)));
            }
            return transactions;
        }
    }
}
