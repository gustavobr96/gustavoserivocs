using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Generics.Result;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Cliente
{

    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly Generics.Interfaces.INotification _Notification;

        public UpdatePasswordCommandHandler(IClientRepository clientRepository, IIdentityRepository identityRepository, Generics.Interfaces.INotification notification)
        {
            _clientRepository = clientRepository;
            _identityRepository = identityRepository;
            _Notification = notification;
        }

        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _clientRepository.GetUserByClientId(request.ClientId);
                if (user != null)
                {
                    user.PasswordHash = EnumExtensions.HashPassword(request.Senha);
                    await _identityRepository.UpdateAsync(user);
                    return new Result(true);
                }

                _Notification.Handle("Erro ao alterar senha, tente mais tarde.");
                return _Notification.Return();
            }
            catch (Exception e)
            {
                _Notification.Handle("Erro ao alterar senha.");
                return _Notification.Return();
            }
        }
    }
}
