using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
            try
            {
                var reader = DatabaseHelper.GetInstance().RetrieveData<Wallet>("SELECT * FROM Wallets where Hash = '" + id + "'", DbModelParser.parseWallet,"");

                return reader.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: api/Wallet
        [HttpPost]
        public string Post([FromBody] string value)
        {
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
            var json = JsonConvert.SerializeObject(new Wallet(hash, 1000));
            return json;
        }

        private void InsertWallet(string hashedWallet)
        {
            var cmd = "INSERT INTO WALLETS VALUES(?wallet,1000)";
            cmd = cmd.Replace("?wallet", "'" + hashedWallet + "'");
            DatabaseHelper.GetInstance().ExecuteSQL(cmd);
        }

    }
}
