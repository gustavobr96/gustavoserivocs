using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IDapperProfessionalClientRepository
    {
        Task<List<ProfessionalClient>> GetMyProfessionalClient(Guid clientId);
    }
}
