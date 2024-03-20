using MediatR;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class ApprovalOrRecusedCommand : IRequest<Unit>
    {
        public Guid ProfessionalClientId { get; set; }
        public bool Aceitar { get; set; }
    }
}
