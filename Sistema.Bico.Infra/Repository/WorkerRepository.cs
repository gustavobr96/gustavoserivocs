using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using Sistema.Bico.Infra.Generics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class WorkerRepository : RepositoryGenerics<Worker>, IWorkerRepository
    {
        private readonly ContextBase _context;
        private readonly IWorkerProfessionalRepository _workerProfessionalRepository;

        public WorkerRepository(ContextBase context,
            IWorkerProfessionalRepository workerProfessionalRepository)
        {
            this._context = context;
            _workerProfessionalRepository = workerProfessionalRepository;
        }

        public async Task<bool> DeleteWorkerId(Guid? id)
        {
            _context.WorkerProfessional.RemoveRange(_context.WorkerProfessional.Where(w => w.WorkerId == id));
            var worker = await _context.Worker.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (worker != null) // Remove worker publicado.
            {
                _context.Worker.Remove(worker);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(int, List<Worker>)> GetProfessionalPagination(FilterWorkerCommand filter)
        {

            var listWorkersApply = await _workerProfessionalRepository.GetWorkerProfessionalByClientId(filter.ClientId);

            var count = await _context.Worker
              .Where(w => (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                     ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                     (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                     (listWorkersApply.Count == 0 || !listWorkersApply.Select(s => s.WorkerId).Contains(w.Id)) && w.ClientId != filter.ClientId)
              .CountAsync();


            var listProfessional = await _context.Worker
                 .Include(es => es.Client)
                 .Include(area => area.ProfessionalArea)
                 .Include(end => end.Address)
                 .Where(w => (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                        ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                        (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                        (listWorkersApply.Count == 0 || !listWorkersApply.Select(s => s.WorkerId).Contains(w.Id)) && w.ClientId != filter.ClientId)
                 .Skip(filter.Take * (filter.Page - 1))
                 .Take(filter.Take)
                 .ToListAsync();

            return (count, listProfessional);
        }

        public async Task<(int, List<Worker>)> GetMyWorkersPagination(FilterWorkerCommand filter)
        {

            var listWorkersApply = await _workerProfessionalRepository.GetWorkerProfessionalByClientId(filter.ClientId);

            var count = await _context.Worker
              .Where(w => (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                     ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                     (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                     (listWorkersApply.Select(s => s.WorkerId).Contains(w.Id)))
              .CountAsync();


            var listProfessional = await _context.Worker
                 .Include(es => es.Client)
                 .Include(area => area.ProfessionalArea)
                 .Include(end => end.Address)
                 .Where(w => (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                        ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                        (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                        (listWorkersApply.Select(s => s.WorkerId).Contains(w.Id)))
                 .Skip(filter.Take * (filter.Page - 1))
                 .Take(filter.Take)
                 .ToListAsync();

            return (count, listProfessional);
        }
        public async Task<(int, List<Worker>)> GetMyPublishWorkerClient(FilterWorkerCommand filter)
        {

            var count = await _context.Worker
              .AsNoTracking()
              .Where(w => (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                     ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                    (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                     w.ClientId == filter.ClientId)
              .CountAsync();


            var listProfessional = await _context.Worker
                 .Include(end => end.WorkerProfessional)
                 .Include(end => end.Address)
                 .AsNoTracking()
                 .Where(w => (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                        ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                        (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                        w.ClientId == filter.ClientId)
                 .Skip(filter.Take * (filter.Page - 1))
                 .Take(filter.Take)
                 .ToListAsync();

            return (count, listProfessional);
        }

        public async Task<List<Worker>> GetMyWorkersClientIdBasic(Guid clientId)
        {
            return await _context.Worker
                 .AsNoTracking()
                 .Where(w => w.ClientId == clientId)
                 .ToListAsync();
        }

    }
}
