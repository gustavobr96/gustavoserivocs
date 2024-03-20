using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Validation;

namespace Sistema.Bico.Domain.Generics.Interfaces
{
    public interface IValidation<in TCommand> where TCommand : CommandBase
    {
        ValidationResult Validate(TCommand command);
    }
}
