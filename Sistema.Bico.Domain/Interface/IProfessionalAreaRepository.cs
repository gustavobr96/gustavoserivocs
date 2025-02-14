﻿using Sistema.Bico.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IProfessionalAreaRepository
    {
        Task<ProfessionalArea> GetProfessionalAreaId(int id);
        Task<List<ProfessionalArea>> GetAllProfessionalArea();
    }
}
