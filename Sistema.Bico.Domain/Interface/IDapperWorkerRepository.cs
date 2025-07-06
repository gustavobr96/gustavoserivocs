using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Response;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IDapperWorkerRepository
    {
        Task<WorkerPaginationResponse> GetMyPublishWorkerClient(FilterWorkerCommand filter);
    }
}
