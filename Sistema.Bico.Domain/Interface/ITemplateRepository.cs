using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface ITemplateRepository
    {
        Task<Template> GetTemplate(TypeTemplate type);
    }
}
