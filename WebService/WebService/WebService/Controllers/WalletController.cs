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
                var reader = DatabaseHelper.GetInstance().RetrieveData<Wallet>("SELECT * FROM Wallets where Hash = '" + id + "'", DbModelParser.ParseWallet, "");

                return reader.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        [HttpPost]
        public string Post(string value)
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
            return hash;
        }

        private void InsertWallet(string hashedWallet)
        {
            var cmd = "INSERT INTO WALLETS VALUES(?wallet,1000)";
            cmd = cmd.Replace("?wallet", "'" + hashedWallet + "'");
            DatabaseHelper.GetInstance().ExecuteSQL(cmd);
            var insertStatement = "INSERT INTO TRANSACTION (FromWallet,ToWallet,Content,Datetime) Values";
            var baseInsertValues = " (?fromWallet,?toWallet,?content,?datetime)";
            var initWalletTx = new Transaction(0, "Gift", hashedWallet, 1000);
            var insertRow = baseInsertValues.Replace("?fromWallet", "'" + initWalletTx.FromWallet + "'");
            insertRow = insertRow.Replace("?toWallet", "'" + initWalletTx.ToWallet + "'");
            insertRow = insertRow.Replace("?content", initWalletTx.Content.ToString());
            insertRow = insertRow.Replace("?content", "'" + initWalletTx.Date + "'");
            insertStatement += insertRow;
            DatabaseHelper.GetInstance().ExecuteSQL(insertStatement);
        }

    }
}
