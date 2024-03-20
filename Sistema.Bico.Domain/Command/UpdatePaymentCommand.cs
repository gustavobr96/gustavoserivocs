using MediatR;
using Sistema.Bico.Domain.Enums;

namespace Sistema.Bico.Domain.Command
{
    public class UpdatePaymentCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public ActionPaymentWebHook Action { get; set; }
    }
}
