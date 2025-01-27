using MediatR;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Response;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class AddPaymentProfessionalCommand : IRequest<string>
    {
        public Guid ClientId { get; set; }
    }
}
