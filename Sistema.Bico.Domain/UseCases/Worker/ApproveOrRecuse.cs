using MediatR;
using Sistema.Bico.Domain.Command;
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

        public ApproveOrRecuseCommandHandler(IProfessionalClientRepository professionalClientRepository)
        {
            _professionalClientRepository = professionalClientRepository;
        }

        public async Task<Unit> Handle(ApprovalOrRecusedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalClientRepository.GetById(request.ProfessionalClientId);
                if (professional != null)
                {
                    var status = request.Aceitar ? StatusWorker.Contratado : StatusWorker.Reprovado;
                    professional.StatusWorker = status;
                    await _professionalClientRepository.Update(professional);
                }

                return Unit.Value;
            }
            catch(Exception e) { return Unit.Value; }
          
        }
    }
}
