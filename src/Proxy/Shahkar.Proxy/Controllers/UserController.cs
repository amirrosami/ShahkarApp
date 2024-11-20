using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shahkar.Proxy.RabbitClient;
using System.Collections.Concurrent;
using System.Text;

namespace Shahkar.Proxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private RabbitRpcClient _rabbitRpcClient;

        public UserController(RabbitRpcClient rabbitRpcClient)
        {
            _rabbitRpcClient = rabbitRpcClient;
        }

        [HttpPut("GetUserInfo")]
        public async Task<ActionResult> Get(GetUserInfoQuery query)
        {
            await _rabbitRpcClient.ListenToReply();
            var response = await _rabbitRpcClient.WaitForReply(query.Phone_Number);
            return Ok(response);
        }
    }


   
}
