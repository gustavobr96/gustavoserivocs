using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class ProfessionalAreaRepository : IProfessionalAreaRepository
    {
        private readonly ContextBase _context;

        public ProfessionalAreaRepository(ContextBase context)
        {
            this._context = context;
        }

        public async Task<ProfessionalArea> GetProfessionalAreaId(int id)
        {
            return await _context.ProfessionalArea
                 .FirstOrDefaultAsync(f => f.Codigo == id);
        }
    }
}
