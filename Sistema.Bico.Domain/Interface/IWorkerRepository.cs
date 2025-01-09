using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IWorkerRepository : IGeneric<Worker>
    {
        Task<(int, List<Worker>)> GetProfessionalPagination(FilterWorkerCommand filter);
        Task<(int, List<Worker>)> GetMyWorkersPagination(FilterWorkerCommand filter);
        Task<(int, List<Worker>)> GetMyPublishWorkerClient(FilterWorkerCommand filter);
        Task<List<Worker>> GetMyWorkersClientIdBasic(Guid clientId);
        Task<bool> DeleteWorkerId(Guid? id);
    }
}
