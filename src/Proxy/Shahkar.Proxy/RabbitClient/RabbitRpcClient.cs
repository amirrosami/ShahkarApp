﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Options;
using Shahkar.Proxy.Configurations;

namespace Shahkar.Proxy.RabbitClient
{
    public class RabbitRpcClient:IAsyncDisposable
    {
        private const string QUEUE_NAME = "user_queue";
        private readonly IConnectionFactory _connectionFactory;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _callbackMapper = new();
        private IConnection? _connection;
        private IChannel? _channel;
        private string? _replyQueueName;
        public RabbitRpcClient(IOptions<RabbitMqConfig> options)
        {
            _connectionFactory = new ConnectionFactory
            {
                UserName = options.Value.UserName,
                HostName = options.Value.HostName,
                Password = options.Value.Password,
                Port = options.Value.Port,
            };
        }

        public async Task ListenToReply()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            QueueDeclareOk queueDeclareResult = await _channel.QueueDeclareAsync();
            _replyQueueName = queueDeclareResult.QueueName;
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                string? correlationId = ea.BasicProperties.CorrelationId;

                if (false == string.IsNullOrEmpty(correlationId))
                {
                    if (_callbackMapper.TryRemove(correlationId, out var tcs))
                    {
                        var body = ea.Body.ToArray();
                        var response = Encoding.UTF8.GetString(body);
                        tcs.TrySetResult(response);
                    }
                }
                return Task.CompletedTask;
            };
            await _channel.BasicConsumeAsync(_replyQueueName, true, consumer);
        }

        public async Task<string> WaitForReply(string message,
            CancellationToken cancellationToken = default)
        {
            if (_channel is null)
            {
                throw new InvalidOperationException();
            }

            string correlationId = Guid.NewGuid().ToString();
            var props = new BasicProperties
            {
                CorrelationId = correlationId,
                ReplyTo = _replyQueueName
            };

            var tcs = new TaskCompletionSource<string>(
                    TaskCreationOptions.RunContinuationsAsynchronously);
            _callbackMapper.TryAdd(correlationId, tcs);

            var messageBytes = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: QUEUE_NAME,
                mandatory: true, basicProperties: props, body: messageBytes);

            using CancellationTokenRegistration ctr =
                cancellationToken.Register(() =>
                {
                    _callbackMapper.TryRemove(correlationId, out _);
                    tcs.SetCanceled();
                });

            return await tcs.Task;
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel is not null)
            {
                await _channel.CloseAsync();
            }

            if (_connection is not null)
            {
                await _connection.CloseAsync();
            }
        }
    }
}