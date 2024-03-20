using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class TermUseRepository : ITermUseRepository
    {
        private readonly ContextBase _context;

        public TermUseRepository(ContextBase context)
        {
            this._context = context;
        }

        public async Task<TermUse> GetProfessionalProfileId(TypeTerm typeTerm)
        {
            return await _context.TermUse
                 .FirstOrDefaultAsync(f => f.TypeTerm == typeTerm && f.Active);
        }
        public async Task Add(TermUse entity)
        {

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
