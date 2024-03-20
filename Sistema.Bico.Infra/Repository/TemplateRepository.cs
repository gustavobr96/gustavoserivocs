using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ContextBase _context;

        public TemplateRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<Template> GetTemplate(TypeTemplate type)
        {
            return await _context.Template
                 .FirstOrDefaultAsync(f => f.TypeTemplate == type);
        }
    }
}
