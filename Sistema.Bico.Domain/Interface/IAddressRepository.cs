using Sistema.Bico.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IAddressRepository
    {
        Task UpdateAddress(Guid id, Address address);
    }
}
