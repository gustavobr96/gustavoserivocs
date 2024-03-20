using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Worker
{
    public class ApplyWorkerCommandHandler : IRequestHandler<ApplyWorkerCommand, Unit>
    {
        private readonly IWorkerProfessionalRepository _workerProfessionalRepository;
        private readonly IProfessionalProfileRepository _professionalProfileRepository;

        public ApplyWorkerCommandHandler(IWorkerProfessionalRepository workerProfessionalRepository,
            IProfessionalProfileRepository professionalProfileRepository)
        {
            _workerProfessionalRepository = workerProfessionalRepository;
            _professionalProfileRepository = professionalProfileRepository;
        }

        public async Task<Unit> Handle(ApplyWorkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalProfileRepository.GetProfessionalProfileId(request.ClientId);
                if (professional != null)
                {
                    var search = await _workerProfessionalRepository.GetWorkerProfessionalByWorker(request.WorkerId, professional.Id);

                    if (search == null)
                        await _workerProfessionalRepository.Add(new WorkerProfessional { WorkerId = request.WorkerId, ProfessionalProfileId = professional.Id });
                }

                return Unit.Value;
            }
            catch (Exception e) { return Unit.Value; }

        }
    }
}
