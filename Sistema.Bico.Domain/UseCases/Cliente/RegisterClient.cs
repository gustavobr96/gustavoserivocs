using AutoMapper;
using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Cliente
{
    public class RegisterClientCommandHandler : IRequestHandler<AddClientCommand, Unit>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITemplateRepository _templateRepository;

        public RegisterClientCommandHandler(IIdentityRepository identityRepository,
            IMapper mapper,
            IMediator mediator,
            ITemplateRepository templateRepository)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _mediator = mediator;
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _mapper.Map<Client>(request);
                var identity = _mapper.Map<ApplicationUser>(request);

                // Set infos
                client.Email = identity.Email;

                if (!string.IsNullOrEmpty(request.FotoBase64))
                    client.PerfilPicture = Convert.FromBase64String(request.FotoBase64);
               
                identity.Client = client;

                var template = await _templateRepository.GetTemplate(TypeTemplate.Cadastro);
                await _identityRepository.RegisterAsync(identity, request.Password);

                await _mediator.Send(new QueuePublishEmailCommand { Email = new EmailDto { To = new List<string> { request.Email }, Subject = TypeSubject.Cadastro.GetDescription(), MessageBody = template.Description }, TypeTemplate = TypeTemplate.Cadastro });

                return Unit.Value;
            }
            catch(Exception e) { return Unit.Value; }
            
        }
    }
}
