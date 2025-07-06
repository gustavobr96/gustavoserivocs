using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UpdateClientTokenCommandHandler> _logger;

        public UpdateClientTokenCommandHandler(IUserDapperRepository userDapperRepository,
           ILogger<UpdateClientTokenCommandHandler> logger )
        {
            _userDapperRepository = userDapperRepository;
            _logger = logger;
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
                _logger.LogError(e, " - Erro ao atualizar token do Cliente");
                return Unit.Value;
            }
        }
    }
}
