using Sistema.Bico.Domain.Command;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IDoneTransactionRepository
    {
        Task DoneWorkerTransaction(DoneWorkerCommand doneWorkerCommand);
    }
}
