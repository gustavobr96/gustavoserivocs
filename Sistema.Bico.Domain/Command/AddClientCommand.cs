using MediatR;
using Sistema.Bico.Domain.Enums;

namespace Sistema.Bico.Domain.Command
{
    public class AddClientCommand : AuthenticationCommandBase, IRequest<Unit>
    {
        public string Name { get; set; }
        public string LastName { get;  set; }
        public TypePeople TypePeople { get;  set; }
        public string CpfCnpj { get;  set; }
        public string FotoBase64 { get;  set; }
        public string TokenPhone { get;  set; }
    }
}
