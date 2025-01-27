using MediatR;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.DePara;
using Sistema.Bico.Domain.Hubs;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("v{version:apiVersion}/api/WebHook")]
    public class WebHookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WebHookController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("payment")]
        [SwaggerOperation(Tags = new[] { "WebHook" })]
        public async Task<IActionResult> WebHookPayment([FromBody] WebhookNotification webHook)
        {
            try
            {
                var idPagamento = webHook?.Data?.Id;

                if (idPagamento == null || idPagamento <= 0)
                {
                    return BadRequest("ID do pagamento inválido na notificação.");
                }

                // Consultar os detalhes do pagamento na API do Mercado Pago
                var paymentDetails = await ObterDetalhesPagamento(idPagamento.Value);

                if (paymentDetails == null)
                {
                    return NotFound("Detalhes do pagamento não encontrados.");
                }


                // Extraindo informações do pagamento
                var status = paymentDetails.Status;
                var metadata = paymentDetails.Metadata;

                if (metadata.TryGetValue("client_id", out var clientId))
                {

                    await _mediator.Send(new UpdatePaymentCommand
                    {
                        IdPagamento = idPagamento.ToString(),
                        Status = status,
                        Notificacao = JsonConvert.SerializeObject(paymentDetails),
                        ClientId = clientId.ToString()
                    });

                    var connections = PaymentHub.GetConnection();
                    var connectionId = PaymentHub.GetConnectionId(clientId.ToString());
                   

                    return Ok();

                }
                else
                {
                    return BadRequest("ClientId não encontrado nos detalhes do pagamento.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar notificação: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        private async Task<Payment> ObterDetalhesPagamento(long paymentId)
        {
            MercadoPagoConfig.AccessToken = "APP_USR-6586471225811590-030913-a2ec9e04767fccea1e46b9a631cf71c4-1326811828";

            var paymentClient = new PaymentClient();
            var payment = await paymentClient.GetAsync(paymentId);

            return payment;
        }
    }
}
