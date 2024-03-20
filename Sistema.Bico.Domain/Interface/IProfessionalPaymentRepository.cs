using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface
{
    public interface IProfessionalPaymentRepository : IGeneric<ProfessionalPayment>
    {
        Task<ProfessionalPayment> GetPaymentProfessional(Guid id);
        Task<ProfessionalPayment> GetPaymentProfessionalByClient(Guid id);
        Task<ProfessionalPayment> GetPaymentProfessionalByPayment(long id);
        Task DeleteDuplicatedOrder(long id);
        Task<int> GetNumberItens(long id);
        Task<List<ProfessionalPayment>> GetPaymentProfessionalPeriod();
    } 
}
