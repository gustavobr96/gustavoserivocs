using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.UseCases.Cliente;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class ApplyProfessionalCommandHandler : IRequestHandler<ApplyProfessionalCommand, Unit>
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly IMediator _mediator;
        private readonly IWorkerRepository _workerRepository;
        private readonly ILogger<ApplyProfessionalCommandHandler> _logger;

        public ApplyProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository,
            IProfessionalClientRepository professionalClientRepository,
            IMediator mediator,
            IWorkerRepository workerRepository,
             ILogger<ApplyProfessionalCommandHandler> logger)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _professionalClientRepository = professionalClientRepository;
            _mediator = mediator;
            _workerRepository = workerRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(ApplyProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalProfileRepository.GetProfessionalPerfil(request.Perfil);
                if (professional != null)
                {
                    var professionalClient = await _professionalClientRepository.GetProfessionalClientEmAndamento(request.ClientId, professional.Id);
                    if (professionalClient == null)
                        await _professionalClientRepository.Add(new ProfessionalClient { ProfessionalProfileId = professional.Id, ClientId = request.ClientId, StatusWorker = StatusWorker.IntencaoServico });
               
                }

                if(request.WorkerId != null)
                {
                    var command = new UpdateProfessionalClientCommand { ClientId = request.ClientId, Perfil = request.Perfil, StatusWorker = StatusWorker.AguardandoConfirmacao };
                    await _mediator.Send(command);
                    await _workerRepository.DeleteWorkerId(request.WorkerId);
                }
            }
            catch(Exception e) 
            {
                _logger.LogError(e, " - Erro ao Aplicar profissional");
                throw;
            }

            return Unit.Value;
        }
    }
}
