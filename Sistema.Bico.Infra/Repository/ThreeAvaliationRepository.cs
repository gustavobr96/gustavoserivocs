using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class ThreeAvaliationRepository : IThreeAvaliationRepository
    {
        private readonly ContextBase _context;

        public ThreeAvaliationRepository(ContextBase context)
        {
            this._context = context;
        }
        public async Task<ThreeAvaliation> GetThreeAvaliationByProfessionalId(Guid professionalId)
        {
            return await _context.ThreeAvaliation.AsNoTracking()
                 .FirstOrDefaultAsync(f => f.ProfessionalProfileId == professionalId);
        }
    }

}
