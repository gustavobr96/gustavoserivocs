using MediatR;
using Sistema.Bico.Domain.Generics.Result;

namespace Sistema.Bico.Domain.Command
{
    public class QueuePublishForgotCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
    }
}
