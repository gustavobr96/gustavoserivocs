using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    //[ApiController]
    //[Route("v{version:apiVersion}/api/teste")]
    //public class TesteController : ControllerBase
    //{
    //    private readonly IMediator _mediator;
    //    private readonly ITemplateRepository _templateRepository;
    //    private const decimal PACKAGE_AMOUNT = 1;

    //    public TesteController(IMediator mediator, ITemplateRepository templateRepository)
    //    {
    //        _mediator = mediator;
    //        _templateRepository = templateRepository;
    //    }

    //    [HttpPost("TesteEmail")]
    //    [SwaggerOperation(Tags = new[] { "Teste" })]
    //    public async Task<IActionResult> TesteEmail(QueuePublishEmailCommand request)
    //    {
    //        var template = await _templateRepository.GetTemplate(TypeTemplate.ConfirmaPagamento);

    //        var dataVencimento = DateTime.UtcNow.AddDays(31);
    //        var messageBody = template.Description.Replace("{DATA_VENCIMENTO}", dataVencimento.ToString("dd/MM/yyyy"));
    //        messageBody = messageBody.Replace("{ID}", "12345667");
    //        messageBody = messageBody.Replace("{VALOR}", EnumExtensions.FormataMoeda(PACKAGE_AMOUNT));

    //        await _mediator.Send(new QueuePublishEmailCommand { Email = new EmailDto { To = new List<string> { "gustavo_barreto7@outlook.com" }, Subject = TypeSubject.PagamentoConfirmado.GetDescription(), MessageBody = messageBody }, TypeTemplate = TypeTemplate.ConfirmaPagamento });
    //        return Ok();
    //    }

    //    [HttpPost("TesteEmailRecovery")]
    //    [SwaggerOperation(Tags = new[] { "Teste" })]
    //    public async Task<IActionResult> TesteEmailRecovery(QueuePublishEmailCommand request)
    //    {
    //        var template = await _templateRepository.GetTemplate(TypeTemplate.RecuperacaoSenha);

    //        var dataVencimento = DateTime.UtcNow.AddDays(31);
    //        var messageBody = template.Description.Replace("{PASSWORD}", "1234");
          

    //        await _mediator.Send(new QueuePublishEmailCommand { Email = new EmailDto { To = new List<string> { "gustavo_barreto7@outlook.com" }, Subject = TypeSubject.RecuperacaoSenha.GetDescription(), MessageBody = messageBody }, TypeTemplate = TypeTemplate.RecuperacaoSenha });
    //        return Ok();
    //    }
    //}
}
