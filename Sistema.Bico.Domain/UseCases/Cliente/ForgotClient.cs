using AutoMapper;
using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Cliente
{
    public class ForgotClientCommandHandler : IRequestHandler<ForgotCommand, Unit>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;
        private readonly ITemplateRepository _templateRepository;

        public ForgotClientCommandHandler(IIdentityRepository identityRepository,
            IMediator mediator,
            IClientRepository clientRepository,
            ITemplateRepository templateRepository)
        {
            _identityRepository = identityRepository;
            _mediator = mediator;
            _clientRepository = clientRepository;
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(ForgotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _clientRepository.GetUserByClientCpfEmail(request.CpfCnpj, request.Email);
                if (user != null)
                {

                    var senha = EnumExtensions.GeneratePassword();
                    user.PasswordHash = EnumExtensions.HashPassword(senha);

                    await _identityRepository.UpdateAsync(user);

                    var template = await _templateRepository.GetTemplate(TypeTemplate.RecuperacaoSenha);
                    var messageBody = template.Description.Replace("{PASSWORD}", senha);

                    //await _mediator.Send(new QueuePublishEmailCommand { Email = new EmailDto { To = new List<string> { request.Email }, Subject = TypeSubject.RecuperacaoSenha.GetDescription(), MessageBody = messageBody }, TypeTemplate = TypeTemplate.RecuperacaoSenha });


                }
            }
            catch (Exception){  return Unit.Value; }

            return Unit.Value;
        }

    }

}
