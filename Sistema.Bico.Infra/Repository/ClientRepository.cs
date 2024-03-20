using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using Sistema.Bico.Infra.Generics.Repository;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class ClientRepository : RepositoryGenerics<Client>, IClientRepository
    {
        private readonly ContextBase _context;

        public ClientRepository(ContextBase context)
        {
            this._context = context;
        }

        // Return Client
        public async Task<ApplicationUser> GetClientFromEmail(string email)
        {
            return await _context.ApplicationUser
                 .Include(c => c.Client)
                 .FirstOrDefaultAsync(f => f.Email == email);
        }
        public async Task<ApplicationUser> GetClientByUserId(string id)
        {
            return await _context.ApplicationUser
                 .Include(c => c.Client)
                 .FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<ApplicationUser> VerifyRegisterUserExist(string email, string cpf)
        {
            return await _context.ApplicationUser
               .FirstOrDefaultAsync(f => string.Equals(f.NormalizedEmail, email.ToUpper()) || string.Equals(f.Client.CpfCnpj, cpf));
        }
        public async Task<ApplicationUser> GetUserByClientId(Guid id)
        {
            return await _context.ApplicationUser
                 .Include(c => c.Client)
                 .FirstOrDefaultAsync(f => f.ClientId == id);
        }

        public async Task<ApplicationUser> GetUserByClientCpfEmail(string cpf, string email)
        {
            return await _context.ApplicationUser
                 .Include(c => c.Client)
                 .FirstOrDefaultAsync(f => f.Email.Equals(email) && f.Client.CpfCnpj.Equals(cpf));
        }
    }
}
