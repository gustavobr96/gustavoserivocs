using Sistema.Bico.Domain.Response;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Generics.Interfaces
{
    public interface IToken
    {
        Task<LoginResponse> GerarJwt(string email);
    }
}
