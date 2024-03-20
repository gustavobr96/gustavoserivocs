using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("v{version:apiVersion}/api/WorkerCancellPlans")]
    public class WorkerCancellPlansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkerCancellPlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("WorkerCancellPlans")]
        [SwaggerOperation(Tags = new[] { "WorkerCancellPlans" })]
        public async Task<IActionResult> WorkerCancellPlans()
        {
            await _mediator.Send(new QueuePublishWorkerCancelPlanCommand());
            return Ok();
        }
    }
}
