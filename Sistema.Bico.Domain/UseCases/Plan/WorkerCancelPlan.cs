using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Plan
{
    public class WorkerCancelPlanCommandHandler : IRequestHandler<WorkerCancellPlansCommand, Unit>
    {
        private readonly IProfessionalPaymentRepository _professionalPaymentRepository;
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IPlanDapperRepository _planDapperRepository;

        public WorkerCancelPlanCommandHandler(IProfessionalPaymentRepository professionalPaymentRepository,
            IProfessionalProfileRepository professionalProfileRepository,
            IPlanDapperRepository planDapperRepository)
        {
            _professionalPaymentRepository = professionalPaymentRepository;
            _professionalProfileRepository = professionalProfileRepository;
            _planDapperRepository = planDapperRepository;
        }

        public async Task<Unit> Handle(WorkerCancellPlansCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professionalsPlans = await _professionalPaymentRepository.GetPaymentProfessionalPeriod();
                if (professionalsPlans.Any())
                {
                    await _planDapperRepository.DeletePlansVenciment();
                    var professional = professionalsPlans.ConvertAll(c => c.Professional);
                    professional.ForEach(p => p.SetEstorno());
                    await _professionalProfileRepository.UpdateStatsProfessionalPlan(professional);
                    return await Task.FromResult(Unit.Value);
                }

                return await Task.FromResult(Unit.Value);
            }
            catch(Exception e) { return Unit.Value; }
           
        }
    }
}
