using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class QueueConsumerRegisterProfessional : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private ConnectionFactory _ConnectionFactory { get; set; }

        public QueueConsumerRegisterProfessional(
             IConnectFactory connectionFactory,
             IServiceScopeFactory serviceScopeFactory)
        {
            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                { "x-message-deduplication", true},
            };

            _serviceScopeFactory = serviceScopeFactory;
            _ConnectionFactory = connectionFactory.Get();
            _connection = _ConnectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                    queue: MessageType.RegisterProfessional.GetDescription(),
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: args
            );

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);
                var _mediator = (IMediator)_serviceScopeFactory.CreateScope().ServiceProvider.GetService(typeof(IMediator));

                consumer.Received += async (sender, eventArgs) =>
                {
                    var contentArray = eventArgs.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var message = JsonConvert.DeserializeObject<AddProfessionalCommand>(contentString);
                    _ = await _mediator.Send(message, stoppingToken);
                };

                _channel.BasicConsume(MessageType.RegisterProfessional.GetDescription(), true, consumer);
            }
            catch (Exception e)
            {
                // TODO IMPLEMENTAR LOG

            }
        }
    }

}
