using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class ProfessionalEspecialityRepository : IProfessionalEspecialityRepository
    {
        private readonly ContextBase _context;

        public ProfessionalEspecialityRepository(ContextBase context)
        {
            this._context = context;
        }

        public async Task UpdateEspecialityProfile(Guid idProfile, List<ProfessionalEspeciality> especiality)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.ProfessionalEspeciality.RemoveRange(_context.ProfessionalEspeciality.Where(x => x.IdProfessionalProfile == idProfile));
                _context.ProfessionalEspeciality.AddRange(especiality.ConvertAll(s => new ProfessionalEspeciality { Description = s.Description, IdProfessionalProfile = idProfile }));
                _context.SaveChanges();
                transaction.Commit();
            }
            catch(Exception e)
            {
                transaction.Rollback();
            }
        }
    }
}
