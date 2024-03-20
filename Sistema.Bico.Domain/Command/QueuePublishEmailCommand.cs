using MediatR;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Result;
using Sistema.Bico.Domain.Response;

namespace Sistema.Bico.Domain.Command
{
    public class QueuePublishEmailCommand : IRequest<Result>
    {
        public EmailDto Email { get; set; }
        public TypeTemplate TypeTemplate { get; set; }
    }
}
