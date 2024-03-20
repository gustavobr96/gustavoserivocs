using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class ApplyProfessionalCommandHandler : IRequestHandler<ApplyProfessionalCommand, Unit>
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IProfessionalClientRepository _professionalClientRepository;

        public ApplyProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository, IProfessionalClientRepository professionalClientRepository)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _professionalClientRepository = professionalClientRepository;
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
            }
            catch(Exception e) { return Unit.Value; }


            return await Task.FromResult(Unit.Value);
        }
    }
}
