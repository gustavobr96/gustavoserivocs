using MediatR;
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
        public UpdateProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository,
            IProfessionalAreaRepository professionalAreaRepository,
            IProfessionalEspecialityRepository professionalEspecialityRepository,
            IAddressRepository addressRepository)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _professionalAreaRepository = professionalAreaRepository;
            _professionalEspecialityRepository = professionalEspecialityRepository;
            _addressRepository = addressRepository;
        }

        public async Task<ProfessionalProfile> Handle(UpdateProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professionalProfile = await _professionalProfileRepository.GetProfessionalProfileId(request.ClientId);

                if (request.Address == null)
                {
                    var professionalArea = await _professionalAreaRepository.GetProfessionalAreaId(request.ProfissionalArea.Codigo);
                    professionalProfile.UpdateProfile(request.Name, request.LastName,request.Phone, request.PerfilPicture, request.About, request.Profession, professionalArea);
                    await _professionalEspecialityRepository.UpdateEspecialityProfile(professionalProfile.Id, request.Especiality);
                }
                else
                    professionalProfile.UpdateAddress(request.Address);
                    
                await _professionalProfileRepository.Update(professionalProfile);
                return professionalProfile;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
