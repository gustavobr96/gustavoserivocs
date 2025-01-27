using MediatR;
using Sistema.Bico.Domain.Enums;

namespace Sistema.Bico.Domain.Command
{
    public class UpdatePaymentCommand : IRequest<Unit>
    {
        public string IdPagamento { get; set; }
        public string Status { get; set; }
        public string Notificacao { get; set; }
        public string ClientId { get; set; }
    }
}
