using MediatR;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.DePara;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.PaymentProfessional
{
    public class RealizarPagamentoCommandHandler : IRequestHandler<AddPaymentProfessionalCommand, long>
    {
        private const string ACCESS_TOKEN = "APP_USR-6586471225811590-030913-a2ec9e04767fccea1e46b9a631cf71c4-1326811828";
        private const string DESCRIPTION = "Plano Premium - WORKFREE";
        private const decimal PACKAGE_AMOUNT = 9.99M;

        private readonly IProfessionalPaymentRepository _professionalPaymentRepository;
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IMediator _mediator;
        private readonly ITemplateRepository _templateRepository;

        public RealizarPagamentoCommandHandler(IProfessionalPaymentRepository professionalPaymentRepository,
            IProfessionalProfileRepository professionalProfileRepository,
            IMediator mediator,
            ITemplateRepository templateRepository)
        {
            _professionalPaymentRepository = professionalPaymentRepository;
            _professionalProfileRepository = professionalProfileRepository;
            _mediator = mediator;
            _templateRepository = templateRepository;
        }

        public async Task<long> Handle(AddPaymentProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalProfileRepository.GetProfessionalProfileIdBasic(request.ClientId);
                var payment30Days = await _professionalPaymentRepository.GetPaymentProfessional(request.ClientId);

                if (professional != null && payment30Days == null)
                {
                    var payment = await CreatePayment(request, professional);

                    if (payment == null)
                        return 0;

                    if (payment.Status.Equals(StatusPayment.REPRO.GetDescription()))
                        return (long)payment.Id;

                    var professionalPayment = new ProfessionalPayment
                    {
                        Value = PACKAGE_AMOUNT,
                        ProfessionalId = professional.Id,
                        PagamentoId = payment.Id,
                        Enable = true,
                        StatusPayment = DePara.DeParaStatusPayment[payment.Status]
                    };

                    if (payment.Status.Equals(StatusPayment.APRO.GetDescription()))
                    {
                        var template = await _templateRepository.GetTemplate(TypeTemplate.ConfirmaPagamento);

                        var dataVencimento = DateTime.UtcNow.AddDays(31);
                        var messageBody = template.Description.Replace("{DATA_VENCIMENTO}", dataVencimento.ToString("dd/MM/yyyy"));
                        //messageBody = messageBody.Replace("{ID}", payment.Id.ToString());
                        messageBody = messageBody.Replace("{VALOR}", EnumExtensions.FormataMoeda(PACKAGE_AMOUNT));

                        await _mediator.Send(new QueuePublishEmailCommand { Email = new EmailDto { To = new List<string> { professional.Client.Email }, Subject = TypeSubject.PagamentoConfirmado.GetDescription(), MessageBody = messageBody }, TypeTemplate = TypeTemplate.ConfirmaPagamento });

                        professional.SetPremium();
                        await _professionalProfileRepository.Update(professional);
                    }

                    await _professionalPaymentRepository.Add(professionalPayment);
                    return (long)payment.Id;
                }

                return 0;
            }
            catch(Exception e) { return 0; }

        }

        private async Task<Payment> CreatePayment(AddPaymentProfessionalCommand request, ProfessionalProfile professional)
        {
            try
            {
                MercadoPagoConfig.AccessToken = ACCESS_TOKEN;

                string cpf = request.Payment.payer.identification?.number;
                string type = request.Payment.payer.identification?.type;

                if (request.TypePayment == TypePayment.Pix)
                {
                    cpf = professional.Client.CpfCnpj;
                    type = "CPF";
                }
               
                var paymentRequest = new PaymentCreateRequest
                {
                    TransactionAmount = PACKAGE_AMOUNT,
                    Token = request.Payment.token,
                    Description = DESCRIPTION,
                    Installments = request.Payment?.installments,
                    PaymentMethodId = request.Payment.payment_method_id,
                    Payer = new PaymentPayerRequest
                    {
                        Email = request.Payment.payer.email,
                        Identification = new IdentificationRequest
                        {
                            Type = cpf,
                            Number = type,
                        }
                    },
                  
                };

                var client = new PaymentClient();
                Payment payment = await client.CreateAsync(paymentRequest);
                return payment;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
