using MediatR;
using Sistema.Bico.Domain.Generics.Result;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class QueueApplyWorkerCommand : IRequest<Result>
    {
        public Guid ClientId { get; set; }
        public Guid WorkerId { get; set; }
    }
}
