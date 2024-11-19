using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shahkar.Consumer;
using System.Text;

const string QUEUE_NAME = "rpc_queue";

var factory = new ConnectionFactory
{
    UserName = "dwkaavkx",
    Uri = new Uri("amqps://dwkaavkx:H__N92KKWdQMWZOOqG3bHfo6ntMXny80@beaver.rmq.cloudamqp.com/dwkaavkx"),
    Password = "H__N92KKWdQMWZOOqG3bHfo6ntMXny80",
    Port = 5671
};
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: QUEUE_NAME, durable: false, exclusive: false,
    autoDelete: false, arguments: null);

await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (object sender, BasicDeliverEventArgs ea) =>
{
    AsyncEventingBasicConsumer cons = (AsyncEventingBasicConsumer)sender;
    IChannel ch = cons.Channel;
    string response = string.Empty;

    byte[] body = ea.Body.ToArray();
    IReadOnlyBasicProperties props = ea.BasicProperties;
    var replyProps = new BasicProperties
    {
        CorrelationId = props.CorrelationId
    };

    try
    {
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"one message Received = {message}");
        response = await ShahkarApiCaller.GetUserAsync(message);
        Console.WriteLine(response);
    }
    catch (Exception e)
    {
        Console.WriteLine($" [.] {e.Message}");
        response = string.Empty;
    }
    finally
    {
        var responseBytes = Encoding.UTF8.GetBytes(response);
        await ch.BasicPublishAsync(exchange: string.Empty, routingKey: props.ReplyTo!,
            mandatory: true, basicProperties: replyProps, body: responseBytes);
        await ch.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
    }
};

await channel.BasicConsumeAsync(QUEUE_NAME, false, consumer);

Console.ReadKey();