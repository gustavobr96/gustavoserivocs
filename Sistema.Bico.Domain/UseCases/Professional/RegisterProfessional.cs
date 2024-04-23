using AutoMapper;
using MediatR;
using Serilog;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class RegisterProfessionalCommandHandler : IRequestHandler<AddProfessionalCommand, Unit>
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IProfessionalAreaRepository _professionalAreaRepository;
        private readonly IMapper _mapper;

        public RegisterProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository,
            IMapper mapper,
            IProfessionalAreaRepository professionalAreaRepository)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _mapper = mapper;
            _professionalAreaRepository = professionalAreaRepository;
        }

        public async Task<Unit> Handle(AddProfessionalCommand request, CancellationToken cancellationToken)
        {
            try
            {
 

                var professionalExist = await _professionalProfileRepository.GetProfessionalProfileIdBasic(request.ClientId);
                if(professionalExist == null)
                {
                    var area = await _professionalAreaRepository.GetProfessionalAreaId(request.ProfissionalArea.Codigo);
                    if (area != null)
                    {
                        var professional = _mapper.Map<ProfessionalProfile>(request);
                        professional.ProfessionalAreaId = area.Id;
                        professional.Perfil = EnumExtensions.GenerateKey();


                        await _professionalProfileRepository.Add(professional);
                    }

                }
                
            }
            catch (Exception e) 
            {
                Log.Information($"{e.Message}");
                Log.Information($"Erro {e.InnerException}");
                Log.Information($"Erro2 {e.ToString()}");

                return Unit.Value;
       
            }


            return await Task.FromResult(Unit.Value);
        }
    }
}
