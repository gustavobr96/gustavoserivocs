using MediatR;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class CancelApplyProfessionalCommand : IRequest<Unit>
    {
        public Guid ClientId { get; set; }
        public string Perfil { get; set; }
    }
}
