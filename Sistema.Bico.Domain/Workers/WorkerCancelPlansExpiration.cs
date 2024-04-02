using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Sistema.Bico.Domain.Command;
using MediatR;

namespace Sistema.Bico.Domain.Workers
{
    public class WorkerCancelPlansExpiration /*: BackgroundService*/
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IMediator _mediator;

        //public WorkerCancelPlansExpiration(IHttpClientFactory httpClientFactory,
        //   IMediator mediator )
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _mediator = mediator;
        //}

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            //await _mediator.Send(new QueuePublishWorkerCancelPlanCommand());

        //            // Aguardar 24 horas antes de chamar o endpoint novamente
        //            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Lidar com erros, como registrar em logs
        //            Console.WriteLine($"Erro ao chamar o worker: {ex.Message}");
        //        }
        //    }
        //}
    }
}
