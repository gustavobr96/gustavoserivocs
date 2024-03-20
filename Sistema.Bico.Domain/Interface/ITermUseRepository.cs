using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface ITermUseRepository
    {
        Task<TermUse> GetProfessionalProfileId(TypeTerm typeTerm);
        Task Add(TermUse entity);
    }
}
