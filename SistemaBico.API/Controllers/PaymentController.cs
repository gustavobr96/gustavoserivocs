using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/api/Payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProfessionalPaymentRepository _professionalPaymentRepository;

        public PaymentController(IMediator mediator,
            IProfessionalPaymentRepository professionalPaymentRepository)
        {
            _mediator = mediator;
            _professionalPaymentRepository = professionalPaymentRepository;
        }

        [HttpPost("Payment")]
        [SwaggerOperation(Tags = new[] { "Payment" })]
        public async Task<IActionResult> Payment(AddPaymentProfessionalCommand addPaymentProfessionalCommand)
        {
            try
            {
                var result = await _mediator.Send(addPaymentProfessionalCommand);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }


        [HttpPost("GetPaymentId")]
        [SwaggerOperation(Tags = new[] { "Payment" })]
        public async Task<IActionResult> GetPaymentId(GuidResponse clientId)
        {
            try
            {
                var response = await _professionalPaymentRepository.GetPaymentProfessionalByClient(clientId.Guid);
                return response != null ? Ok(response.PagamentoId) :  Ok(0);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

    }
}
