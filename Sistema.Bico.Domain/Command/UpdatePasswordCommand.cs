using MediatR;
using Sistema.Bico.Domain.Generics.Result;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class UpdatePasswordCommand : IRequest<Result>
    {
        public string Senha { get; set; }
        public Guid ClientId { get; set; }
    }
}
