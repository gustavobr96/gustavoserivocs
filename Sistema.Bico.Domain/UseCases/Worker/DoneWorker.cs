using MediatR;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Worker
{
    public class DoneWorkerCommandHandler : IRequestHandler<DoneWorkerCommand, Unit>
    {
        private readonly IDoneTransactionRepository _doneTransactionRepository;
        private readonly ILogger<DoneWorkerCommandHandler> _logger;
        public DoneWorkerCommandHandler(IDoneTransactionRepository doneTransactionRepository, ILogger<DoneWorkerCommandHandler> logger)
        {
            _doneTransactionRepository = doneTransactionRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DoneWorkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _doneTransactionRepository.DoneWorkerTransaction(request);
                return Unit.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, " - Erro ao concluir serviço");
                throw;
            }

        }
    }
}
