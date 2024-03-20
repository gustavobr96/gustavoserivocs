using Sistema.Bico.Domain.Generics.Notification;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Generics.Interfaces
{
    public interface IResult
    {
        void AddMessages(IEnumerable<Notifier> messages);
    }
}
