
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Generics.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Sistema.Bico.Domain.Generics.Notification
{
    public class Notification : INotification
    {
        private List<Notifier> _Notifier;

        public Notification()
        {
            _Notifier = new List<Notifier>();
        }

        public IEnumerable<Notifier> GetNotifications()
            => _Notifier;

        public void Handle(string message)
            => _Notifier.Add(new Notifier(message));

        public bool HasNotifications()
            => GetNotifications().Any();
        public Domain.Generics.Result.Result Return()
        {
            var result = new Domain.Generics.Result.Result();

            if (HasNotifications())
                result.Success = false;

            result.AddMessages(GetNotifications());
            return result;
        }
        public Domain.Generics.Result.Result Return(object value)
           => new Domain.Generics.Result.Result(value);


        ValidationResult INotification.Validate<T, TValidation>(T command, TValidation validation)
        {
            var result = validation.Validate(command);
            foreach (var validationMessage in result.Messages)
            {
                if (!string.IsNullOrEmpty(validationMessage.Message))
                    Handle(validationMessage.Message);
            }

            return result;
        }
    }
}
