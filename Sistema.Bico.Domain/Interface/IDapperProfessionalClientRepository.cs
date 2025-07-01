using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IDapperProfessionalClientRepository
    {
        Task<List<ProfessionalClient>> GetMyProfessionalClient(Guid clientId);
        Task AtualizarStatus(Guid id, StatusWorker status);
    }
}
