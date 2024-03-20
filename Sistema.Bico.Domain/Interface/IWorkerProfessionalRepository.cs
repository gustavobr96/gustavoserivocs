using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IWorkerProfessionalRepository : IGeneric<WorkerProfessional>
    {
        Task<WorkerProfessional> GetWorkerProfessionalByWorker(Guid workerId, Guid professionalProfileId);
        Task<List<WorkerProfessional>> GetWorkerProfessionalByClientId(Guid clientId);
    }
}
