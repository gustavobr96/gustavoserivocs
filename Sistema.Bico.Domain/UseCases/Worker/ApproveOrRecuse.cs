using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Worker
{
    public class ApproveOrRecuseCommandHandler : IRequestHandler<ApprovalOrRecusedCommand, Unit>
    {
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly IDapperProfessionalClientRepository _professionalClientDapperRepository;

        public ApproveOrRecuseCommandHandler(IProfessionalClientRepository professionalClientRepository,
            IDapperProfessionalClientRepository professionalClientDapperRepository)
        {
            _professionalClientRepository = professionalClientRepository;
            _professionalClientDapperRepository = professionalClientDapperRepository;
        }

        public async Task<Unit> Handle(ApprovalOrRecusedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalClientRepository.GetById(request.ProfessionalClientId);
                if (professional != null)
                {
                    var status = request.Aceitar ? StatusWorker.Contratado : StatusWorker.Reprovado;

                    await _professionalClientDapperRepository.AtualizarStatus(professional.Id, status);
                }

                return Unit.Value;
            }
            catch(Exception e) { return Unit.Value; }
          
        }
    }
}
