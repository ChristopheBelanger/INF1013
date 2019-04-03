using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.Containers;
using WebService.Database;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        // POST: api/Client
        [HttpPost]
        public void Post([FromBody] Transaction value)
        {
            TransactionPool.addTx(value);
        }

        // POST: api/Client
        [HttpGet]
        public List<Transaction> Get()
        {
            var result = DatabaseHelper.GetInstance().RetrieveData("SELECT * FROM TRANSACTION");
            var transactions = DbModelParser.parseTransaction(result);
            return transactions;

        }

    }
}
