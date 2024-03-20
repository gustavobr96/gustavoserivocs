using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.DePara;
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
        private readonly ITermUseRepository _termUseRepository;

        public WebHookController(IMediator mediator, ITermUseRepository termUseRepository)
        {
            _mediator = mediator;
            _termUseRepository = termUseRepository;
        }

        [HttpPost("WebHook")]
        [SwaggerOperation(Tags = new[] { "WebHook" })]
        public async Task<IActionResult> WebHook(WebHookDto webHook)
        {
            var termo = new TermUse { Description = JsonConvert.SerializeObject(webHook), TypeTerm = TypeTerm.TermUseProfessional, Version = 1 };
            await _termUseRepository.Add(termo);
            
            await _mediator.Send(new QueueAddPaymentCommand { Id = long.Parse(webHook.data.id), Action = DePara.DeParaActionWebHook[webHook.action] });
            return Ok();
        }
    }
}
