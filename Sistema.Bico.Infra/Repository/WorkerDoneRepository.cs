using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class WorkerDoneRepository : IWorkerDoneRepository
    {
        private readonly ContextBase _context;

        public WorkerDoneRepository(ContextBase context)
        {
            this._context = context;
        }

        public async Task<(int, List<WorkerDone>)> GetWorkerDoneByProfilePagination(FilterProfileCommand filter)
        {
            var count = await _context.WorkerDone
             .AsNoTracking()
             .Where(w => w.WorkerDoneProfessional.ProfessionalProfile.Perfil == filter.Profile)
             .CountAsync();

            var listWorkerDone = await _context.WorkerDone
                .AsNoTracking()
                .Where(f => f.WorkerDoneProfessional.ProfessionalProfile.Perfil == filter.Profile)
                .Skip(filter.Take * (filter.Page - 1))
                .Take(filter.Take)
                .ToListAsync();

            return (count, listWorkerDone);
        }
        public async Task<List<WorkerDone>> GetListWorkerDoneProfile(string id)
        {
            var list = await _context.WorkerDone
             .AsNoTracking()
             .Where(w => w.WorkerDoneProfessional.ProfessionalProfile.Perfil == id)
             .ToListAsync();

            return list;
        }
    }
}
