using MediatR;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Response;

namespace Sistema.Bico.Domain.Command
{
    public class SendEmailCommand : IRequest<Unit>
    {
        public EmailDto Email { get; set; }
        public TypeTemplate TypeTemplate { get; set; }
    }
}
