using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Cliente
{
    public class UpdateClientTokenCommandHandler : IRequestHandler<UpdateClientTokenCommand, Unit>
    {
        private readonly IUserDapperRepository _userDapperRepository;

        public UpdateClientTokenCommandHandler(IUserDapperRepository userDapperRepository)
        {
            _userDapperRepository = userDapperRepository;
        }

        public async Task<Unit> Handle(UpdateClientTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userDapperRepository.AtualizarTokenPhone(request.ClientId, request.Token);
                return Unit.Value;
            }
            catch (Exception e)
            {
                return Unit.Value;
            }
        }
    }
}
