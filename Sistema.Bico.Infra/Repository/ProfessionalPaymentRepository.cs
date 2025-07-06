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
    public class ProfessionalPaymentRepository : RepositoryGenerics<ProfessionalPayment>, IProfessionalPaymentRepository
    {

        private readonly ContextBase _context;

        public ProfessionalPaymentRepository(ContextBase context)
        {
            this._context = context;
        }
        public async Task<List<ProfessionalPayment>> GetPaymentProfessionalPeriod()
        {
            return await _context.ProfessionalPayment
                 .Include(i => i.Professional)
                 .Where(w => w.Created.AddDays(31) <= DateTime.Now && w.Status == StatusPayment.APRO.GetDescription())
                 .ToListAsync();
        }

        public async Task<ProfessionalPayment> GetPaymentProfessional(Guid id)
        {
            return await _context.ProfessionalPayment
                 .Where(w => w.ProfessionalId == id && w.Created.AddDays(31) >= DateTime.Now && w.Status == StatusPayment.APRO.GetDescription())
                 .FirstOrDefaultAsync();
        }

        public async Task<ProfessionalPayment> GetPaymentProfessionalByClient(Guid id)
        {
            return await _context.ProfessionalPayment
                 .AsNoTracking()
                 .Where(w => w.Professional.ClientId == id && w.Created.AddDays(31) >= DateTime.Now)
                 .OrderByDescending(o => o.Created)
                 .FirstOrDefaultAsync();
        }

        public async Task<ProfessionalPayment> GetPaymentProfessionalByPayment(string id)
        {
            return await _context.ProfessionalPayment
                 .Where(w => w.PagamentoId == id)
                 .FirstOrDefaultAsync();
        }

    }
}
