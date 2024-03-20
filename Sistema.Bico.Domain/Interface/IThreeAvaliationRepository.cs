using Sistema.Bico.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IThreeAvaliationRepository
    {
        Task<ThreeAvaliation> GetThreeAvaliationByProfessionalId(Guid professionalId);
    }
}
