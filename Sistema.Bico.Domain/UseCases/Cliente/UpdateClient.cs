using MediatR;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Cliente
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ApplicationUser>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly ILogger<UpdateClientCommandHandler> _logger;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IIdentityRepository identityRepository, ILogger<UpdateClientCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _identityRepository = identityRepository;
            _logger = logger;
        }

        public async Task<ApplicationUser> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await  _clientRepository.GetUserByClientId(request.ClientId);
                user.PhoneNumber = request.PhoneNumber;

                user.Client.UpdateProfile(request.Client.Name, request.Client.LastName, request.FotoBase64);
                await _identityRepository.UpdateAsync(user);

                return user;
            }
            catch(Exception e)
            {
                _logger.LogError(e, " - Erro ao atualizar Cliente");
                return null;
            }
        }
    }
}
