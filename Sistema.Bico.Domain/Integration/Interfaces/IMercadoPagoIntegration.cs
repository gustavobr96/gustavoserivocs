using MercadoPago.Resource.Payment;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Integration.Interfaces
{
    public interface IMercadoPagoIntegration
    {
        Task<Payment> GetPayment(long id);
    }
}
