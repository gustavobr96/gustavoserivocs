using MediatR;
using Serilog;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.DePara;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Integration.Interfaces;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.PaymentProfessional
{
    public class AtualizaStatusPagamentoCommandHandler : IRequestHandler<UpdatePaymentCommand, Unit>
    {
        private readonly IMercadoPagoIntegration _mercadoPago;
        private readonly IProfessionalPaymentRepository _professionalPaymentRepository;
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IMediator _mediator;

        public AtualizaStatusPagamentoCommandHandler(IMercadoPagoIntegration mercadoPago,
            IProfessionalPaymentRepository professionalPaymentRepository,
            IProfessionalProfileRepository professionalProfileRepository,
            IMediator mediator,
            ITemplateRepository templateRepository)
        {
            _mercadoPago = mercadoPago;
            _professionalPaymentRepository = professionalPaymentRepository;
            _professionalProfileRepository = professionalProfileRepository;
            _mediator = mediator;
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Action == ActionPaymentWebHook.CREATE)
                {
                    var count = await _professionalPaymentRepository.GetNumberItens(request.Id);
                    if (count == 1)
                        return Unit.Value;

                    await _professionalPaymentRepository.DeleteDuplicatedOrder(request.Id);
                }

                var payment = await _mercadoPago.GetPayment(request.Id);
                var professionalPayment = await _professionalPaymentRepository.GetPaymentProfessionalByPayment(request.Id);
                StatusPayment statusPayment = new StatusPayment();
                try
                {
                    statusPayment = DePara.DeParaStatusPayment[payment.Status];
                }
                catch (Exception e)
                {
                    Log.Error($"UpdatePaymentCommand Error: {e.Message}");
                    return Unit.Value;
                }

                professionalPayment.StatusPayment = statusPayment;
                if (statusPayment == StatusPayment.APRO || statusPayment == StatusPayment.ESTORNO)
                {
                    var professional = await _professionalProfileRepository.GetProfessionalProfileById(professionalPayment.ProfessionalId);

                    if (statusPayment == StatusPayment.APRO)
                    {
                        professional.SetPremium();
                        //var template = await _templateRepository.GetTemplate(TypeTemplate.ConfirmaPagamento);
                        //var dataVencimento = DateTime.UtcNow.AddDays(31);
                        //var messageBody = template.Description.Replace("{DATA_VENCIMENTO}", dataVencimento.ToString("dd/MM/yyyy"));
                        //messageBody = messageBody.Replace("{ID}", payment.Id.ToString());
                        //messageBody = messageBody.Replace("{VALOR}", EnumExtensions.FormataMoeda(professionalPayment.Value));

                        //await _mediator.Send(new QueuePublishEmailCommand { Email = new EmailDto { To = new List<string> { professional.Client.Email }, Subject = TypeSubject.PagamentoConfirmado.GetDescription(), MessageBody = messageBody }, TypeTemplate = TypeTemplate.ConfirmaPagamento });

                    }
                    else
                        professional.SetEstorno();

                    await _professionalProfileRepository.Update(professional);
                }

                await _professionalPaymentRepository.Update(professionalPayment);
                return Unit.Value;

            }catch(Exception e)
            {
                Log.Error($"UpdatePaymentCommand Error: {e.Message}"); 
                return Unit.Value;
            }
           
        }
    }

}
