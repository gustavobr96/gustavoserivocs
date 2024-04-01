using MediatR;
using RabbitMQ.Client;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Plan
{

    public class QueuePublishWorkerCancelPlanCommandHandler : IRequestHandler<QueuePublishWorkerCancelPlanCommand, Unit>
    {
        private ConnectionFactory _ConnectionFactory { get; set; }

        public QueuePublishWorkerCancelPlanCommandHandler(IConnectFactory connectionFactory)
        {
            _ConnectionFactory = connectionFactory.Get();
        }

        public async Task<Unit> Handle(QueuePublishWorkerCancelPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = _ConnectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var queueName = MessageType.WorkerCancelPlan.GetDescription();

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

                        return Unit.Value;
                    }
                }
            }
            catch (Exception e)
            {
                return Unit.Value;
            }
        }
    }
}
