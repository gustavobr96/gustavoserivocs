using MediatR;

namespace Sistema.Bico.Domain.Command
{
    public class ForgotCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
    }
}
