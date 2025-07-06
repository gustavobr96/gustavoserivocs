using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
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

        public async Task<(int, List<WorkerResponse>)> GetProfessionalPagination(FilterWorkerCommand filter)
        {
            var listWorkersApply = await _workerProfessionalRepository.GetWorkerProfessionalByClientId(filter.ClientId);

            // IDs dos workers que o cliente já aplicou
            var appliedIds = listWorkersApply.Select(s => s.WorkerId).ToList();

            // Filtros comuns
            var queryBase = _context.Worker
                .AsNoTracking()
                .Where(w =>
                    (!filter.Remote || w.Remote) &&
                    (filter.Remote || string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                    ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                    (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                    (!appliedIds.Any() || !appliedIds.Contains(w.Id)) &&
                    w.ClientId != filter.ClientId);

            // Count com cache
            var count = await queryBase
                .Cacheable()
                .CountAsync();

            // Dados paginados, apenas com os campos necessários
            var list = await queryBase
                .Select(w => new WorkerResponse
                {
                    Id = w.Id,
                    Created = w.Created.ToString("yyyy-MM-dd"),
                    Name = w.Client.Name,
                    Price = w.Price,
                    Titulo = w.Title,
                    Phone = w.Phone,
                    Area = w.ProfessionalArea.Codigo,
                    AreaName = w.ProfessionalArea.Description,
                    Remote = w.Remote,
                    Sobre = w.About,
                    Profession = w.Profession,
                    CEP = w.Address.ZipCode,
                    City = w.Address.City,
                    State = w.Address.State,
                    Interessados = _context.WorkerProfessional.Count(x => x.WorkerId == w.Id)
                })
                .Skip(filter.Take * (filter.Page - 1))
                .Take(filter.Take)
                .Cacheable()
                .ToListAsync();

            return (count, list);
        }

        public async Task<(int, List<WorkerResponse>)> GetMyWorkersPagination(FilterWorkerCommand filter)
        {
            var listWorkersApply = await _workerProfessionalRepository
                .GetWorkerProfessionalByClientId(filter.ClientId);

            var workerIds = listWorkersApply.Select(s => s.WorkerId).ToList();

            // Filtro base
            var query = _context.Worker
                .AsNoTracking()
                .Where(w =>
                    (string.IsNullOrEmpty(filter.City) || w.Address.City.Contains(filter.City)) &&
                    ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                    (string.IsNullOrEmpty(filter.Profession) || w.Profession.Contains(filter.Profession)) &&
                    workerIds.Contains(w.Id)
                );

            var count = await query.CountAsync();

            var list = await query
                .OrderByDescending(w => w.Created)
                .Skip(filter.Take * (filter.Page - 1))
                .Take(filter.Take)
                .Select(w => new WorkerResponse
                {
                    Id = w.Id,
                    Created = w.Created.ToString("yyyy-MM-dd"),
                    Name = w.Client.Name,
                    Price = w.Price,
                    Titulo = w.Title,
                    Phone = w.Phone,
                    Area = w.ProfessionalArea.Codigo,
                    AreaName = w.ProfessionalArea.Description,
                    Remote = w.Remote,
                    Sobre = w.About,
                    Profession = w.Profession,
                    CEP = w.Address.ZipCode,
                    City = w.Address.City,
                    State = w.Address.State,
                    Interessados = w.WorkerProfessional.Count()
                })
                .ToListAsync();

            return (count, list);
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
