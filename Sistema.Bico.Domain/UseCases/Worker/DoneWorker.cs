using MediatR;
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
        public DoneWorkerCommandHandler(IDoneTransactionRepository doneTransactionRepository)
        {
           _doneTransactionRepository = doneTransactionRepository;
        }

        public async Task<Unit> Handle(DoneWorkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _doneTransactionRepository.DoneWorkerTransaction(request);
                return Unit.Value;
            }
            catch(Exception e) { return Unit.Value; }
           
        }
    }
}
