using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IClientRepository : IGeneric<Client>
    {
        Task<ApplicationUser> GetClientFromEmail(string email);
        Task<ApplicationUser> GetClientByUserId(string id);
        Task<ApplicationUser> GetUserByClientId(Guid id);
        Task<ApplicationUser> VerifyRegisterUserExist(string email, string cpf);
        Task<ApplicationUser> GetUserByClientCpfEmail(string cpf, string email);
    }
}
