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
        Task<ProfessionalPayment> GetPaymentProfessionalByPayment(string id);
        Task<List<ProfessionalPayment>> GetPaymentProfessionalPeriod();
    } 
}
