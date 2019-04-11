using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.Containers;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {

        // POST: api/Connection
        [HttpPost]
        public string[] Post([FromBody] string ip)
        {
            var connectedNodes = ConnectionStore.getConnectedNodes();
            ConnectionStore.Connect(ip);
            return connectedNodes;
        }


        // DELETE: api/Connection/5
        [HttpDelete("{ip}")]
        public void Delete(string ip)
        {
            ConnectionStore.Disconnect(ip);
        }
    }
}
