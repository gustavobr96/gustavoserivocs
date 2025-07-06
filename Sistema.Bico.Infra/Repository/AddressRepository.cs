using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ContextBase _context;

        public AddressRepository(ContextBase context)
        {
            this._context = context;
        }
        public async Task UpdateAddress(Guid id, Address address)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Address.FirstOrDefaultAsync(x => x.Id == id);
                if (existing != null)
                {
                    _context.Address.Remove(existing);
                }

                await _context.Address.AddAsync(address);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }
    }
}
