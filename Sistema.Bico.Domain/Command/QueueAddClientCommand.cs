using MediatR;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Result;

namespace Sistema.Bico.Domain.Command
{
    public class QueueAddClientCommand : AuthenticationCommandBase, IRequest<Result>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public TypePeople TypePeople { get; set; }
        public string CpfCnpj { get; set; }
    }
}
