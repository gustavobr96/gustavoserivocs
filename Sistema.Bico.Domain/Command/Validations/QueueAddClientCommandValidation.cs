using FluentValidation;
using Sistema.Bico.Domain.Generics.Entities;

namespace Sistema.Bico.Domain.Command.Validations
{
    public class QueueAddClientCommandValidation : AbstractValidator<QueueAddClientCommand>
    {
        public QueueAddClientCommandValidation()
        {
            RuleFor(c => c.Email)
             .EmailAddress().WithMessage("E-mail é obrigatório")
             .Length(1, 100).WithMessage("Apenas de 1 a 100 caracteres");

            RuleFor(c => c.PhoneNumber)
             .Must((o, phoneNumber) => { return ItemsGenerics.ValidatePhone(phoneNumber); })
             .WithMessage(StringError._ERRORTELEFONE);

            RuleFor(c => c.CpfCnpj)
             .Must((o, cpfCnpj, typePeople) => { return ItemsGenerics.ValidCpfCnpj(cpfCnpj,o.TypePeople); })
             .WithMessage(StringError._ERRORCNPJCPF);
        }      
    }
}
