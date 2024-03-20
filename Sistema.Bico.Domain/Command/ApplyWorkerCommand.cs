using MediatR;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class ApplyWorkerCommand : IRequest<Unit>
    {
        public Guid ClientId { get; set; }
        public Guid WorkerId { get; set; }
    }

}
