using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {

        // GET: api/Stats/5
        [HttpGet("{id}", Name = "GetStats")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
