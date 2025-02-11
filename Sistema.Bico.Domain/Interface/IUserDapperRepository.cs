using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IUserDapperRepository
    {
         Task AtualizarTokenPhone(Guid id, string tokenPhone);
    }
}
