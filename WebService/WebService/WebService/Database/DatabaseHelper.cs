using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebService.Database
{
    public class DatabaseHelper
    {
        private static DatabaseHelper Database;
        private MySqlConnection Connection;
        private object DbLock = new object();

        public DatabaseHelper(string connectionString) {
            Connection = new MySqlConnection(connectionString);
            Database = this;
        }


        public static DatabaseHelper GetInstance() {
            return Database;
        }

        public delegate T ParserFunction<T>(MySqlDataReader reader, string id);

        public List<T> RetrieveData<T>(string Query, ParserFunction<List<T>> f,string id)
        {
            lock (DbLock)
            {
                var cmd = Connection.CreateCommand();
                cmd.CommandText = Query;
                try
                {
                    Connection.Open();
                    var reader = cmd.ExecuteReader();
                    List<T> obj = f(reader, id);
                    Connection.Close();
                    return obj;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void ExecuteSQL(string query)
        {
            lock (DbLock)
            {
                var cmd = Connection.CreateCommand();
                Connection.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
        }
    }
}
