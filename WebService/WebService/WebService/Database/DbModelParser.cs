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
        public static List<Wallet> parseWallet(MySqlDataReader reader) {
            var wallets = new List<Wallet>();
            while (reader.Read())
            {
                wallets.Add(new Wallet(reader.GetFieldValue<string>(0), reader.GetFieldValue<double>(1)));
            }
            return wallets;
        }

        public static List<Transaction> parseTransaction(MySqlDataReader reader)
        {
            var transactions = new List<Transaction>();   
            while (reader.Read())
            {
                transactions.Add(new Transaction(reader.GetFieldValue<int>(0), reader.GetFieldValue<string>(1), reader.GetFieldValue<string>(2), reader.GetFieldValue<double>(3)));
            }
            return transactions;
        }
    }
}
