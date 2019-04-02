using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.Containers;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // GET api/Transaction
        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetTransaction()
        {
            return TransactionPool.FetchTransactions();
        }

        // POST api/Transaction
        [HttpPost]
        public void Post([FromBody] long[] txId)
        {
            TransactionPool.FinishTx(txId);
        }
    }
}