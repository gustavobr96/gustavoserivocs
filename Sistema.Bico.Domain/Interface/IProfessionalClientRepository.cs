using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IProfessionalClientRepository : IGeneric<ProfessionalClient>
    {
        Task<ProfessionalClient> GetProfessionalClient(Guid clientId, Guid professionalId);
        Task<List<ProfessionalClient>> GetMyProfessionalClient(Guid clientId);
        Task<ProfessionalClient> GetProfessionalClientByProfile(Guid clientId, string profile);
        Task<ProfessionalClient> GetProfessionalClientByProfileIntencao(Guid clientId, string profile);
        Task<List<ProfessionalClient>> GetClientApproval(Guid clientId);
        Task<ProfessionalClient> GetById(Guid id);
        Task<ProfessionalClient> GetProfessionalClientEmAndamento(Guid clientId, Guid professionalId);
    }
}
