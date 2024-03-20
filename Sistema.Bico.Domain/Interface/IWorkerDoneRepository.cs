using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IWorkerDoneRepository
    {
        Task<(int, List<WorkerDone>)> GetWorkerDoneByProfilePagination(FilterProfileCommand filter);
    }
}
