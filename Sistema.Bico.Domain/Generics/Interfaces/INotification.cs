using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Notification;
using Sistema.Bico.Domain.Generics.Validation;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Generics.Interfaces
{
    public interface INotification
    {
        void Handle(string message);
        IEnumerable<Notifier> GetNotifications();
        bool HasNotifications();
        Domain.Generics.Result.Result Return();
        ValidationResult Validate<T, TValidation>(T command, TValidation validation) where T : CommandBase where TValidation : IValidation<T>;
        Domain.Generics.Result.Result Return(object value);
    }
}
