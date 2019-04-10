﻿using System;
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

        // POST: api/Wallet
        [HttpPost]
        public string Post()
        {
            var crypt = new SHA256Managed();
            Random random = new Random();
            var r = random.Next(1001, 99999);
            string salt = CalculateMD5Hash(r + "");
            string hash = String.Empty;
            var currDate = DateTime.Now;
            string timeStamp = currDate.ToString("yyyyMMddHHmmssffff");
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(salt + timeStamp));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            InsertWallet(hash);
            var json = JsonConvert.SerializeObject(new Wallet(hash, 1000));
            return hash;
        }

        private void InsertWallet(string hashedWallet)
        {
            var cmd = "INSERT INTO WALLETS VALUES(?wallet,1000)";
            cmd = cmd.Replace("?wallet", "'" + hashedWallet + "'");
            DatabaseHelper.GetInstance().ExecuteSQL(cmd);
        }

    }
}
