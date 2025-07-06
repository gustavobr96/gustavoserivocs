using MediatR;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class UpdateProfessionalCommandHandler : IRequestHandler<UpdateProfessionalCommand, ProfessionalProfile>
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IProfessionalAreaRepository _professionalAreaRepository;
        private readonly IProfessionalEspecialityRepository _professionalEspecialityRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<UpdateProfessionalCommandHandler> _logger;
        public UpdateProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository,
            IProfessionalAreaRepository professionalAreaRepository,
            IProfessionalEspecialityRepository professionalEspecialityRepository,
            IAddressRepository addressRepository,
             ILogger<UpdateProfessionalCommandHandler> logger)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _professionalAreaRepository = professionalAreaRepository;
            _professionalEspecialityRepository = professionalEspecialityRepository;
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<ProfessionalProfile> Handle(UpdateProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professionalProfile = await _professionalProfileRepository.GetProfessionalProfileId(request.ClientId);
                if (professionalProfile == null)
                {
                    throw new ArgumentException("Perfil profissional não encontrado para o ID fornecido.");
                }

                var professionalArea = await _professionalAreaRepository.GetProfessionalAreaId(request.ProfissionalArea.Codigo);
                professionalProfile.UpdateProfile(request.Name, request.LastName, request.Phone, request.PerfilPicture, request.About, request.Profession, professionalArea);
                await _professionalEspecialityRepository.UpdateEspecialityProfile(professionalProfile.Id, request.Especiality);
                professionalProfile.UpdateAddress(request.Address);

                await _professionalProfileRepository.Update(professionalProfile);
                return professionalProfile;
            }
            catch (Exception e)
            {
                _logger.LogError(e, " - Erro ao Atualizar profissional");
                throw;
            }
        }
    }
}
