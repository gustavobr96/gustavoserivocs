using MediatR;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using Serilog;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.PaymentProfessional
{
    public class RealizarPagamentoCommandHandler : IRequestHandler<AddPaymentProfessionalCommand, string>
    {
        private const string ACCESS_TOKEN = "APP_USR-6586471225811590-030913-a2ec9e04767fccea1e46b9a631cf71c4-1326811828";
        private const string DESCRIPTION = "Plano Premium - BICO";
        private const decimal PACKAGE_AMOUNT = 1.00M;

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

        public async Task<string> Handle(AddPaymentProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalProfileRepository.GetProfessionalProfileIdBasic(request.ClientId);

                if(professional != null)
                {
                    MercadoPagoConfig.AccessToken = ACCESS_TOKEN;

                    var preferenceRequest = new PreferenceRequest
                    {
                        Items = new List<PreferenceItemRequest>
                        {
                            new PreferenceItemRequest
                            {
                                Title = DESCRIPTION,
                                Quantity = 1,
                                CurrencyId = "BRL",
                                UnitPrice = PACKAGE_AMOUNT
                            }
                        },
                        Payer = new PreferencePayerRequest
                        {
                            Email = professional.Client.Email
                        },
                        BackUrls = new PreferenceBackUrlsRequest
                        {
                            Success = "https://sucesso.com",
                            Failure = "https://falha.com",
                            Pending = "https://pendente.com",
                        },
                        AutoReturn = StatusPayment.APRO.GetDescription(),
                        Metadata = new Dictionary<string, object>
                        {
                            { "ClientId", request.ClientId }
                        }
                    };
                    var preferenceClient = new PreferenceClient();
                    var response = await preferenceClient.CreateAsync(preferenceRequest);

                    // Verificando se a resposta foi bem-sucedida
                    if (response != null && response.InitPoint != null)
                    {
                        return response.InitPoint;
                    }
                    else
                    {
                        throw new Exception("Erro ao criar a preferência de pagamento.");
                    }
                }

            }
            catch (Exception e)
            {
                Log.Error($"Exception Payment: {e.Message}");
           
            }

            return "";
        }
    }
}
