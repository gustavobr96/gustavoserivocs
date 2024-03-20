using MediatR;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Result;

namespace Sistema.Bico.Domain.Command
{
    public class QueueAddPaymentCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public ActionPaymentWebHook Action { get; set; }
    }
}
