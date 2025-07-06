using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IDapperProfessionalClientRepository
    {
        Task<(int Total, List<ProfessionalProfile> List)> GetProfessionalPaginationWithSlapper(FilterProfessionalCommand filter);
        Task AtualizarStatus(Guid id, StatusWorker status);
    }
}
