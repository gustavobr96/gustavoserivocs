using MediatR;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class UpdateClientCommand : IRequest<ApplicationUser>
    {
        public string PhoneNumber { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
