using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Generics.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Generics.Result
{
    public class Result : IResult
    {
        private readonly IList<string> _messages;
        public IEnumerable<string> Messages => _messages;
        public bool Success { get; set; }
        public object Data { get; set; }

        public Result()
        {
            _messages = new List<string>();
        }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(object data)
        {
            Success = true;
            Data = data;
        }

        public void AddMessages(IEnumerable<Notifier> messages)
        {
            foreach (var validationMessage in messages)
            {
                if (!string.IsNullOrEmpty(validationMessage.Message))
                    _messages.Add(validationMessage.Message);
            }
        }
    }
}
