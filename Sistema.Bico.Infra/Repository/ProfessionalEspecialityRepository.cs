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
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existentes = _context.ProfessionalEspeciality.Where(x => x.IdProfessionalProfile == idProfile);
                _context.ProfessionalEspeciality.RemoveRange(existentes);

                var novasEspecialidades = especiality
                    .ConvertAll(s => new ProfessionalEspeciality
                    {
                        Description = s.Description,
                        IdProfessionalProfile = idProfile
                    });

                await _context.ProfessionalEspeciality.AddRangeAsync(novasEspecialidades);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

    }
}
