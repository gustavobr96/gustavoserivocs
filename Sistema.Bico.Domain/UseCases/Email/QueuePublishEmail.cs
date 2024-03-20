﻿using MediatR;
using RabbitMQ.Client;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Generics.Result;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Email
{

    public class QueuePublishEmailCommandHandler : IRequestHandler<QueuePublishEmailCommand, Result>
    {
        private readonly Generics.Interfaces.INotification _Notification;
        private ConnectionFactory _ConnectionFactory { get; set; }

        public QueuePublishEmailCommandHandler(Generics.Interfaces.INotification Notification,
            IConnectFactory connectionFactory)
        {
            _Notification = Notification;
            _ConnectionFactory = connectionFactory.Get();
        }

        public async Task<Result> Handle(QueuePublishEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = _ConnectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var queueName = MessageType.SendEmail.GetDescription();

                        //declare Queue
                        channel.QueueDeclare(
                            queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        // Format Data 
                        string stringMessage = JsonSerializer.Serialize(request);
                        var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                        // Publish
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queueName,
                            basicProperties: null,
                            body: byteArray
                            );

                        return new Result(true);
                    }
                }
            }
            catch (Exception e)
            {
                _Notification.Handle("Erro ao enfileirar email");
                return _Notification.Return();
            }
        }
    }
}
