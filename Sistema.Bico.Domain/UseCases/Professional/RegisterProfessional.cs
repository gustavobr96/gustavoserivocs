﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RegisterProfessionalCommandHandler> _logger;

        public RegisterProfessionalCommandHandler(IProfessionalProfileRepository professionalProfileRepository,
            IMapper mapper,
            IProfessionalAreaRepository professionalAreaRepository,
            ILogger<RegisterProfessionalCommandHandler> logger)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _mapper = mapper;
            _professionalAreaRepository = professionalAreaRepository;
            _logger = logger;
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

                        if (!string.IsNullOrEmpty(request.PerfilPicture))
                            professional.PerfilPicture = Convert.FromBase64String(request.PerfilPicture);
     
                        professional.Perfil = EnumExtensions.GenerateKey();
                        await _professionalProfileRepository.Add(professional);
                    }

                }
                
            }
            catch (Exception e) 
            {
                _logger.LogError(e, " - Erro ao Aplicar profissional");
                throw;
            }

            return Unit.Value;
        }
    }
}
