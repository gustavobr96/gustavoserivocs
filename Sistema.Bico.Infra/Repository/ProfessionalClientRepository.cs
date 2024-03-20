using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using Sistema.Bico.Infra.Generics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class ProfessionalClientRepository : RepositoryGenerics<ProfessionalClient>, IProfessionalClientRepository
    {
        private readonly ContextBase _context;

        public ProfessionalClientRepository(ContextBase context)
        {
            this._context = context;
        }
        public async Task<ProfessionalClient> GetProfessionalClient(Guid clientId, Guid professionalId)
        {
            return await _context.ProfessionalClient
                 .Where(w => w.ClientId == clientId && w.ProfessionalProfileId == professionalId && w.StatusWorker != StatusWorker.Reprovado)
                 .FirstOrDefaultAsync();
        }

        public async Task<ProfessionalClient> GetProfessionalClientEmAndamento(Guid clientId, Guid professionalId)
        {
            return await _context.ProfessionalClient
                 .Where(w => w.ClientId == clientId && w.ProfessionalProfileId == professionalId && EnumExtensions.GetStatusEmAndamento().Contains(w.StatusWorker))
                 .FirstOrDefaultAsync();
        }

        public async Task<ProfessionalClient> GetById(Guid id)
        {
            return await _context.ProfessionalClient
                 .Where(w => w.Id == id && w.StatusWorker == StatusWorker.AguardandoConfirmacao)
                 .FirstOrDefaultAsync();
        }
        public async Task<ProfessionalClient> GetProfessionalClientByProfile(Guid clientId, string profile)
        {
            //var professional = await _context.ProfessionalProfile.FirstOrDefaultAsync(f => f.Perfil == profile);
            return  await _context.ProfessionalClient
                    .FirstOrDefaultAsync(f => f.ClientId == clientId  && f.ProfessionalProfile.Perfil == profile);
        }
        public async Task<ProfessionalClient> GetProfessionalClientByProfileIntencao(Guid clientId, string profile)
        {
            //var professional = await _context.ProfessionalProfile.FirstOrDefaultAsync(f => f.Perfil == profile);
            return await _context.ProfessionalClient
                    .Where(w => w.StatusWorker == StatusWorker.IntencaoServico)
                    .FirstOrDefaultAsync(f => f.ClientId == clientId && f.ProfessionalProfile.Perfil == profile);
        }



        public async Task<List<ProfessionalClient>> GetMyProfessionalClient(Guid clientId)
        {
            return await _context.ProfessionalClient
                 .Include(s => s.ProfessionalProfile)
                 .Include(s => s.ProfessionalProfile.Address)
                 .Include(s => s.ProfessionalProfile.Especiality)
                 .Include(s => s.Client)
                 .Where(f => f.ClientId == clientId)
                 .OrderByDescending(x => x.Created)
                 .ToListAsync();
    
        }

        public async Task<List<ProfessionalClient>> GetClientApproval(Guid clientId)
        {
            return await _context.ProfessionalClient
                 .Include(s => s.Client)
                 .Include(s => s.Client.ApplicationUser)
                 .Where(f => f.ProfessionalProfile.ClientId == clientId && f.StatusWorker == StatusWorker.AguardandoConfirmacao)
                 .OrderBy(c => c.StatusWorker)
                 .ToListAsync();

        }
    }
}
