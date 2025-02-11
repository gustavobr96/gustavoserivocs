using MediatR;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class UpdateClientTokenCommand : IRequest<Unit>
    {
        public Guid ClientId { get; set; }
        public string Token { get; set; }
    }
}
