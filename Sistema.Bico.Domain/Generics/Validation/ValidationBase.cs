using FluentValidation;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;

namespace Sistema.Bico.Domain.Generics.Validation
{
    public class ValidationBase<T> : AbstractValidator<T>, IValidation<T> where T : CommandBase
    {
        public ValidationResult Validate(T command) => base.Validate(command).GetResult();
    }

    public static class ValidationResultExtensions
    {
        public static ValidationResult GetResult(this global::FluentValidation.Results.ValidationResult result)
        {
            var validationResult = new ValidationResult();
            foreach (var message in result.Errors)
                validationResult.AddMessage(message.ErrorMessage);

            return validationResult;
        }
    }
}
