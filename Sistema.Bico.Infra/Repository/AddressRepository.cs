using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System;
using System.Linq;
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
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Address.Remove(_context.Address.Where(x => x.Id == id).FirstOrDefault());
                _context.Address.AddRange(address);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
        }
    }
}
