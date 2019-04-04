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
        public static List<Wallet> parseWallet(MySqlDataReader reader, string id) {
            var wallets = new List<Wallet>();
            while (reader.Read())
            {
                wallets.Add(new Wallet(reader.GetFieldValue<string>(0), (double)reader.GetFieldValue<decimal>(1)));
            }
            return wallets;
        }

        public static List<Transaction> parseTransaction(MySqlDataReader reader, string id)
        {
            var transactions = new List<Transaction>();   
            while (reader.Read())
            {
                transactions.Add(new Transaction(reader.GetFieldValue<int>(0), reader.GetFieldValue<string>(1), reader.GetFieldValue<string>(2), (double)reader.GetFieldValue<decimal>(3)));
            }
            return transactions;
        }

        public static List<TransactionHistory> parseTransactionHistory(MySqlDataReader reader, string id)
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
                transactions.Add(new TransactionHistory(reader.GetFieldValue<int>(0), action, wallet, (double)reader.GetFieldValue<decimal>(3)));
            }
            return transactions;
        }
    }
}
