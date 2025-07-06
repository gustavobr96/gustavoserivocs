using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
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
            return await _context.ProfessionalClient
                    .FirstOrDefaultAsync(f => f.ClientId == clientId && f.ProfessionalProfile.Perfil == profile);
        }

        public async Task<ProfessionalClient> GetProfessionalClientByProfileIntencao(Guid clientId, string profile)
        {
            return await _context.ProfessionalClient
                    .Where(w => w.StatusWorker == StatusWorker.IntencaoServico)
                    .FirstOrDefaultAsync(f => f.ClientId == clientId && f.ProfessionalProfile.Perfil == profile);

        }

        public async Task<List<ProfessionalClientResponse>> GetMyProfessionalClient(Guid clientId)
        {
            return await _context.ProfessionalClient
                           .AsNoTracking()
                           .Where(f => f.ClientId == clientId)
                           .Select(s => new ProfessionalClientResponse
                           {
                               Id = s.Id,
                               ProfessionalProfile = new ProfessionalProfileResponse
                               {
                                   Name = s.ProfessionalProfile.Name,
                                   Profession = s.ProfessionalProfile.Profession,
                                   City = s.ProfessionalProfile.Address.City,
                                   CEP = s.ProfessionalProfile.Address.ZipCode,
                                   Phone = s.ProfessionalProfile.Phone,
                                   LastName = s.ProfessionalProfile.LastName,
                                   Perfil = s.ProfessionalProfile.Perfil,
                               },
                               StatusWorker = s.StatusWorker,
                               Created = s.Created
                           })
                           .OrderByDescending(x => x.Created)
                           .Take(10)
                           .ToListAsync();
        }

        public async Task<ProfessionalClientResponse> GetMyProfessionalClientByPerfil(string Perfil)
        {
            return await _context.ProfessionalClient
                           .AsNoTracking()
                           .Where(f => f.ProfessionalProfile.Perfil == Perfil)
                           .Select(s => new ProfessionalClientResponse
                           {
                               Id = s.Id,
                               ProfessionalProfile = new ProfessionalProfileResponse
                               {
                                   Name = s.ProfessionalProfile.Name,
                                   Profession = s.ProfessionalProfile.Profession,
                                   City = s.ProfessionalProfile.Address.City,
                                   CEP = s.ProfessionalProfile.Address.ZipCode,
                                   Phone = s.ProfessionalProfile.Phone,
                                   LastName = s.ProfessionalProfile.LastName,
                                   Especiality = s.ProfessionalProfile.Especiality.Select(s => s.Description).ToList(),
                                   Sobre = s.ProfessionalProfile.About,
                                   PerfilPicture = s.ProfessionalProfile.PerfilPicture != null ? Convert.ToBase64String(s.ProfessionalProfile.PerfilPicture) : Base64Default._imageDefault,
                                   Perfil = s.ProfessionalProfile.Perfil,
                                   Avaliation = s.ProfessionalProfile.Avaliation != null ? s.ProfessionalProfile.Avaliation.ToString() : "0,0",
                               },
                               StatusWorker = s.StatusWorker,
                               Created = s.Created
                           })
                           .FirstOrDefaultAsync();
        }

        public async Task<List<ProfessionalClientResponse>> GetClientApproval(Guid clientId)
        {
            return await _context.ProfessionalClient
                .AsNoTracking()
                .Where(f =>
                    f.ProfessionalProfile.ClientId == clientId &&
                    f.StatusWorker == StatusWorker.AguardandoConfirmacao)
                .OrderBy(c => c.StatusWorker)
                .Select(pc => new ProfessionalClientResponse
                {
                    Id = pc.Id,
                    StatusWorker = pc.StatusWorker,
                    Created = pc.Created,
                    Client = new ClientResponse
                    {
                        NameComplete = pc.Client.Name ?? string.Empty,
                        PhoneNumber = pc.Client.ApplicationUser.FirstOrDefault().PhoneNumber ?? string.Empty,
                        PerfilPicture = pc.Client.PerfilPicture != null
                            ? Convert.ToBase64String(pc.Client.PerfilPicture)
                            : Base64Default._imageDefault
                    }
                })
                .ToListAsync();
        }
    }
}
