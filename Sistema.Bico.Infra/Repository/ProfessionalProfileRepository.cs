using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using Sistema.Bico.Infra.Generics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class ProfessionalProfileRepository : RepositoryGenerics<ProfessionalProfile>, IProfessionalProfileRepository
    {
        private readonly ContextBase _context;

        public ProfessionalProfileRepository(ContextBase context)
        {
            this._context = context;
        }

        public async Task UpdateStatsProfessionalPlan(List<ProfessionalProfile> list)
        {
            _context.ProfessionalProfile.UpdateRange(list);
            await _context.SaveChangesAsync();
        }

        public async Task<ProfessionalProfile> GetProfessionalProfileById(Guid id)
        {
            return await _context.ProfessionalProfile
                 .Include(i => i.Client)
                 .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<ProfessionalProfile> GetProfessionalProfileId(Guid id)
        {
            return await _context.ProfessionalProfile
                 .Include(es => es.Especiality)
                 .Include(es => es.Client)
                 .Include(area => area.ProfessionalArea)
                 .Include(end => end.Address)
                 .FirstOrDefaultAsync(f => f.ClientId == id);

        }

        public async Task<ProfessionalProfile> GetVerifyProfissional(Guid id)
        {
            return await _context.ProfessionalProfile
                 .Include(es => es.Client)
                 .FirstOrDefaultAsync(f => f.ClientId == id);
        }
        public async Task<ProfessionalProfile> GetProfessionalPerfilId(string id)
        {
            return await _context.ProfessionalProfile
                 .Include(es => es.Especiality)
                 .Include(es => es.Client)
                 .Include(area => area.ProfessionalArea)
                 .Include(end => end.Address)
                 .FirstOrDefaultAsync(f => f.Perfil == id);
        }

        public async Task<ProfessionalProfile> GetProfessionalPerfil(string perfil)
        {
            return await _context.ProfessionalProfile
                 .FirstOrDefaultAsync(f => f.Perfil == perfil);
        }

        public async Task<List<ProfessionalProfile>> GetTopProfessional(string? city = null)
        {
            return await _context.ProfessionalProfile
                  .Include(end => end.Address)
                  .Where(w => string.IsNullOrEmpty(city) || w.Address.City.Contains(city) && w.Ativo)
                  .Take(12)
                  .ToListAsync();
        }

        public async Task<List<ProfessionalProfile>> GetProfessionalByAreaAndCity(string? city, int area)
        {
            return await _context.ProfessionalProfile
                  .Include(end => end.Client)
                  .Where((w => string.IsNullOrEmpty(city) || w.Address.City.Contains(city) && w.ProfessionalArea.Codigo == area && w.Client.TokenPhone != null))
                  .Cacheable()
                  .ToListAsync();
        }



        public async Task<ProfessionalProfile> GetProfessionalProfileIdBasic(Guid id)
        {
            return await _context.ProfessionalProfile
                 .Include(es => es.Client)
                 .FirstOrDefaultAsync(f => f.ClientId == id);
        }

        public async Task<(int, List<ProfessionalProfile>)> GetProfessionalPagination(FilterProfessionalCommand filter)
        {
            // Definindo uma chave exclusiva para o cache, considerando os filtros e a paginação
            var cacheKey = $"ProfessionalProfiles_Page_{filter.Page}_Take_{filter.Take}_ClientId_{filter.ClientId}_City_{filter.City}_Area_{filter.Area}_Profession_{filter.Profession}_Especiality_{string.Join(",", filter.Especiality ?? new List<string>())}";

            // Consulta para contar os registros (não cacheada, já que não envolve dados paginados)
            var count = await _context.ProfessionalProfile
                .Where(w =>
                    (string.IsNullOrEmpty(filter.City) || w.Address.City.Trim().ToLower().Contains(filter.City.Trim().ToLower())) &&
                    ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                    ((filter.Especiality == null || filter.Especiality.Count == 0) || w.Especiality.Any(l => filter.Especiality.Contains(l.Description))) &&
                    w.ClientId != filter.ClientId && w.Ativo &&
                    (string.IsNullOrEmpty(filter.Profession) || w.Profession.ToLower().Contains(filter.Profession.ToLower())))
                .CountAsync();

            // Consulta para obter os registros paginados, com cache de segundo nível
            var listProfessional = await _context.ProfessionalProfile
                .Include(es => es.Especiality)
                .Include(es => es.Client)
                .Include(area => area.ProfessionalArea)
                .Include(end => end.Address)
                .Where(w =>
                    (string.IsNullOrEmpty(filter.City) || w.Address.City.Trim().ToLower().Contains(filter.City.Trim().ToLower())) &&
                    ((filter.Area == null || filter.Area == 0) || w.ProfessionalArea.Codigo == filter.Area) &&
                    ((filter.Especiality == null || filter.Especiality.Count == 0) || w.Especiality.Any(l => filter.Especiality.Contains(l.Description))) &&
                    w.ClientId != filter.ClientId && w.Ativo &&
                    (string.IsNullOrEmpty(filter.Profession) || w.Profession.ToLower().Contains(filter.Profession.ToLower())))
                .Skip(filter.Take * (filter.Page - 1))
                .Take(filter.Take)
                .Cacheable() // 🔥 Ativando o cache para a consulta paginada
                .ToListAsync();

            return (count, listProfessional);
        }


        public async Task<List<ProfessionalProfile>> GetProfessionalInterested(Guid workerId)
        {
            var listProfessional = await _context.ProfessionalProfile
                 .Include(es => es.Especiality)
                 .Include(es => es.Client)
                 .Include(end => end.Address)
                 .AsNoTracking()
                 .Where(w => w.WorkerProfessional.Where(wh => wh.WorkerId == workerId).Any(a => a.ProfessionalProfileId == w.Id))
                 .ToListAsync();

            return listProfessional;
        }
        public async Task<ProfessionalProfile> GetProfessionalProfileBasic(string id)
        {
            return await _context.ProfessionalProfile
                 .FirstOrDefaultAsync(f => f.Perfil == id);
        }

        public async Task<ProfessionalProfile> GetProfessionalPerfilClient(string perfil)
        {
            return await _context.ProfessionalProfile
                  .Include(x => x.Client)
                 .FirstOrDefaultAsync(f => f.Perfil == perfil);
        }
    }
}
