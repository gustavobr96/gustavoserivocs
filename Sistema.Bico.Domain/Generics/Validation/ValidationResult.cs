using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Generics.Validation
{
    public class ValidationResult
    {
        private readonly IList<ValidationMessage> _messages;
        public IEnumerable<ValidationMessage> Messages => _messages;
        public bool IsValid => !Messages.Any();

        public ValidationResult()
        {
            _messages = new List<ValidationMessage>();
        }

        public void AddMessage(string message)
        {
            _messages.Add(new ValidationMessage(message));
        }
    }

    public class ValidationMessage
    {
        public string Message { get; private set; }

        private ValidationMessage()
        {
        }

        public ValidationMessage(string message) : this()
        {
            Message = message;
        }
    }
}
