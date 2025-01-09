using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IProfessionalProfileRepository : IGeneric<ProfessionalProfile>
    {
        Task<ProfessionalProfile> GetProfessionalProfileId(Guid id);
        Task<(int, List<ProfessionalProfile>)> GetProfessionalPagination(FilterProfessionalCommand filter);
        Task<ProfessionalProfile> GetProfessionalProfileIdBasic(Guid id);
        Task<List<ProfessionalProfile>> GetProfessionalInterested(Guid workerId);
        Task<ProfessionalProfile> GetProfessionalPerfilId(string id);
        Task<ProfessionalProfile> GetProfessionalPerfil(string id);
        Task<ProfessionalProfile> GetProfessionalProfileById(Guid id);
        Task UpdateStatsProfessionalPlan(List<ProfessionalProfile> list);
        Task<List<ProfessionalProfile>> GetTopProfessional(string? city = null);
        Task<ProfessionalProfile> GetProfessionalProfileBasic(string perfil);
    }
}
