using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shahkar.ApiGateway.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Shahkar.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> GetUser(GetUserInfoQuery query)
        {
            object message = null;
            string requestQueueName = "RequestUserQueue";
            string responseQueueName = "ResponseUserQueue";
            var factory = new ConnectionFactory() { HostName = "localhost"};
            using var connection = await factory.CreateConnectionAsync();
           using var channel = await connection.CreateChannelAsync();
            
          var responseQueue = await channel.QueueDeclareAsync(queue: responseQueueName, durable: false, exclusive: false,autoDelete: false, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, e) =>
            {
                var body = e.Body.ToArray();
                message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));
                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync(responseQueue, true, consumer);

            // publish
           var requestQueue = await channel.QueueDeclareAsync(requestQueueName,durable:false,exclusive:false);
            string correlationId = Guid.NewGuid().ToString();
            var props = new BasicProperties
            {
                CorrelationId = correlationId,
                ReplyTo = responseQueue.QueueName
            };

            byte[] publishMessage = Encoding.UTF8.GetBytes(query.Phone_Number);
            await channel.BasicPublishAsync(exchange:"",routingKey: requestQueue,
                mandatory: true,basicProperties: props,body: publishMessage);


            return Ok(message);
        }

    }
}
