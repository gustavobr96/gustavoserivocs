using MediatR;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{

    public class CancelApplyProfessionalCommandHandler : IRequestHandler<CancelApplyProfessionalCommand, Unit>
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly ILogger<CancelApplyProfessionalCommandHandler> _logger;

        public CancelApplyProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository,
            IProfessionalClientRepository professionalClientRepository,
            ILogger<CancelApplyProfessionalCommandHandler> logger)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _professionalClientRepository = professionalClientRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelApplyProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professional = await _professionalProfileRepository.GetProfessionalPerfil(request.Perfil);
                if (professional != null)
                {
                    var professionalClient = await _professionalClientRepository.GetProfessionalClientEmAndamento(request.ClientId, professional.Id);
                    if (professionalClient != null)
                        await _professionalClientRepository.Delete(professionalClient);

                }

            }
            catch(Exception e)
            {
                _logger.LogError(e, " - Erro recusar profissional");
                throw;
            }
          
            return Unit.Value;
        }
    }
}
