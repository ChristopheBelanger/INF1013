using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebService.Database;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        // GET: api/Wallet/5
        [HttpGet("{id}", Name = "GetWallet")]
        public Wallet GetWallet(string id)
        {
            var reader = DatabaseHelper.GetInstance().RetrieveData("SELECT * FROM Wallets where Wallet = '" + id + "'");

            var hasValue = reader.Read();
            if (hasValue) {
                return new Wallet(reader.GetFieldValue<string>(0), reader.GetFieldValue<double>(1));
            }
            return null;
        }

        // POST: api/Wallet
        [HttpPost]
        public string Post([FromBody] string value)
        {
            string newWallet = "";
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            var currDate = DateTime.Now;
            string timeStamp = currDate.ToString("yyyyMMddHHmmssffff");
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(value + timeStamp));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            InsertWallet(hash);
            return newWallet;
        }

        private void InsertWallet(string hashedWallet) {
            var cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO WALLETS VALUES(?wallet,1000)";
            cmd.Parameters.AddWithValue("?wallet", hashedWallet);
            DatabaseHelper.GetInstance().ExecuteSQL(cmd);
        }

    }
}
