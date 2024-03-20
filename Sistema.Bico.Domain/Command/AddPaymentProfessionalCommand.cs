using MediatR;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Response;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class AddPaymentProfessionalCommand : IRequest<long>
    {
        public Guid ClientId { get; set; }
        public TypePayment TypePayment { get; set; }
        public PaymentResponse Payment { get; set; }
    }
}
