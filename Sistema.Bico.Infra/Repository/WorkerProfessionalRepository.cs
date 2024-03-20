using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using Sistema.Bico.Infra.Generics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class WorkerProfessionalRepository : RepositoryGenerics<WorkerProfessional>, IWorkerProfessionalRepository
    {
        private readonly ContextBase _context;

        public WorkerProfessionalRepository(ContextBase context)
        {
            this._context = context;
        }
        public async Task<WorkerProfessional> GetWorkerProfessionalByWorker(Guid workerId, Guid professionalProfileId)
        {
            return await _context.WorkerProfessional
                 .FirstOrDefaultAsync(f => f.WorkerId == workerId && f.ProfessionalProfileId == professionalProfileId);
        }

        public async Task<List<WorkerProfessional>> GetWorkerProfessionalByClientId(Guid clientId)
        {
            return await _context.WorkerProfessional
                 .Where(f => f.ProfessionalProfile.ClientId == clientId)
                 .ToListAsync();
        }
    }
}
