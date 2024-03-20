using Sistema.Bico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IProfessionalEspecialityRepository
    {
        Task UpdateEspecialityProfile(Guid idProfile, List<ProfessionalEspeciality> especiality);
    }
}
