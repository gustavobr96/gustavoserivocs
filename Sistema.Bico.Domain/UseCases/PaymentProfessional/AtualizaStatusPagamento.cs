﻿using MediatR;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Integration.Interfaces;
using Sistema.Bico.Domain.Interface;
using System;
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
        private readonly ILogger<AtualizaStatusPagamentoCommandHandler> _logger;

        public AtualizaStatusPagamentoCommandHandler(IMercadoPagoIntegration mercadoPago,
            IProfessionalPaymentRepository professionalPaymentRepository,
            IProfessionalProfileRepository professionalProfileRepository,
            IMediator mediator,
            ITemplateRepository templateRepository,
            ILogger<AtualizaStatusPagamentoCommandHandler> logger
            )
        {
            _mercadoPago = mercadoPago;
            _professionalPaymentRepository = professionalPaymentRepository;
            _professionalProfileRepository = professionalProfileRepository;
            _mediator = mediator;
            _templateRepository = templateRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pagamento = await _professionalPaymentRepository.GetPaymentProfessionalByPayment(request.IdPagamento);
                var professional = await _professionalProfileRepository.GetProfessionalProfileId(Guid.Parse(request.ClientId));

                if (pagamento != null)
                {
                    pagamento.Status = request.Status;
                    pagamento.Detalhes = request.Notificacao;
                    pagamento.Update = DateTime.Now;
                    pagamento.ProfessionalId = professional.Id;

                    await _professionalPaymentRepository.Update(pagamento);
                }
                else
                {
                    var novoPagamento = new ProfessionalPayment
                    {
                        PagamentoId = request.IdPagamento,
                        Status = request.Status,
                        Detalhes = request.Notificacao,
                        ProfessionalId = professional.Id,
                        Created = DateTime.Now,
                        Update = DateTime.Now
                    };

                    await _professionalPaymentRepository.Add(novoPagamento);
                }

                if (request.Status == StatusPayment.APRO.GetDescription())
                {
                    professional.SetPremium();
                    await _professionalProfileRepository.Update(professional);
                }else
                    if (request.Status == StatusPayment.ESTORNO.GetDescription())
                    {
                        professional.SetEstorno();
                        await _professionalProfileRepository.Update(professional);
                    }

                return Unit.Value;

            }
            catch(Exception e)
            {
                _logger.LogError(e, " - Erro ao atualizar status do pagamento");
                return Unit.Value;
            }
           
        }


    }

}
