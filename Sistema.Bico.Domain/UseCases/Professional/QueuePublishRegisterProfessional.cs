using MediatR;
using RabbitMQ.Client;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Generics.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class QueuePublishRegisterProfessionalCommandHandler : IRequestHandler<QueuePublishRegisterProfessionalCommand, Result>
    {
        private readonly Generics.Interfaces.INotification _Notification;
        private ConnectionFactory _ConnectionFactory { get; set; }
        public QueuePublishRegisterProfessionalCommandHandler(Generics.Interfaces.INotification notification, IConnectFactory connectionFactory)
        {
            _Notification = notification;
            _ConnectionFactory = connectionFactory.Get();
        }

        public async Task<Result> Handle(QueuePublishRegisterProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = _ConnectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        Dictionary<string, object> args = new Dictionary<string, object>()
                        {
                            { "x-deduplication-header", request.ClientId.ToString()},
                            { "x-cache-size", 1000},
                            { "x-cache-ttl", 10000},
                            { "x-cache-persistence", "memory"},
                            { "x-message-deduplication", true},
                        };

                        var queueName = MessageType.RegisterProfessional.GetDescription();

                        //declare Queue
                        channel.QueueDeclare(
                            queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: args );

                        channel.ExchangeDeclare($"exchange-{queueName}", "x-message-deduplication", false, true, args);
                        channel.QueueBind($"{queueName}", $"exchange-{queueName}", queueName);
                        // Format Data 
                        string stringMessage = JsonSerializer.Serialize(request);
                        var byteArray = Encoding.UTF8.GetBytes(stringMessage);
                        var properties = channel.CreateBasicProperties();
                        properties.Headers = args;

                        // Publish
                        channel.BasicPublish(
                            exchange: $"exchange-{queueName}",
                            routingKey: queueName,
                            basicProperties: properties,
                            body: byteArray
                            );

                        return new Result(true);
                    }
                }
            }
            catch (Exception e)
            {
                _Notification.Handle("Erro ao enfileirar o cancel apply professional!");
                return _Notification.Return();
            }
        }
    }

}
