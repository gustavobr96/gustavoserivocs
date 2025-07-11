﻿using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
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
        public async Task<ProfessionalProfileResponse> GetProfessionalProfileIdTracking(Guid id)
        {
            var perfil = await _context.ProfessionalProfile
                .AsNoTracking()
                .Where(p => p.ClientId == id)
                .Select(p => new ProfessionalProfileResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameComplete = p.Name + " " + p.LastName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Perfil = p.Perfil,
                    Avaliation = p.Avaliation == null ? "0,0" : p.Avaliation.ToString(),
                    PerfilPicture = p.PerfilPicture.Length > 0 ? Convert.ToBase64String(p.PerfilPicture) : Base64Default._imageDefault,
                    Especiality = p.Especiality.Select(e => e.Description).ToList(),
                    Area = p.ProfessionalArea.Codigo,
                    AreaName = p.ProfessionalArea.Description,
                    Sobre = p.About,
                    Profession = p.Profession,
                    Logradouro = p.Address.Logradouro,
                    Number = p.Address.Number,
                    Bairro = p.Address.Bairro,
                    State = p.Address.State,
                    Complemento = p.Address.Complemento,
                    CEP = p.Address.ZipCode,
                    City = p.Address.City,
                    Email = p.Client.Email,
                    Ativo = p.Ativo,
                    Premium = p.Premium,
                })
                .FirstOrDefaultAsync();

            return perfil;
        }


        public async Task<ProfessionalProfile> GetVerifyProfissional(Guid id)
        {
            return await _context.ProfessionalProfile
                 .AsNoTracking()
                 .Include(es => es.Client)
                 .FirstOrDefaultAsync(f => f.ClientId == id);
        }
        public async Task<ProfessionalProfile> GetProfessionalPerfilId(string id)
        {
            return await _context.ProfessionalProfile
                 .AsNoTracking()
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
                  .AsNoTracking()
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
                 .AsNoTracking()
                 .FirstOrDefaultAsync(f => f.Perfil == id);
        }

        public async Task<ProfessionalProfile> GetProfessionalPerfilClient(string perfil)
        {
            return await _context.ProfessionalProfile
                 .AsNoTracking()
                 .Include(x => x.Client)
                 .FirstOrDefaultAsync(f => f.Perfil == perfil);
        }

       

    }
}
