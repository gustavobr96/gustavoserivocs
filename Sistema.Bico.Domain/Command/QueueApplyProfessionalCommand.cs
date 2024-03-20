using MediatR;
using Sistema.Bico.Domain.Generics.Result;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class QueueApplyProfessionalCommand : IRequest<Result>
    {
        public Guid ClientId { get; set; }
        public string Perfil { get; set; }
    }
}
