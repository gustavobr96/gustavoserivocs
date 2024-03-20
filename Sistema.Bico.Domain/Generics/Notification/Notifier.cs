namespace Sistema.Bico.Domain.Generics.Notification
{
    public class Notifier
    {
        public string Message { get; private set; }

        public Notifier(string message)
        {
            Message = message;
        }
    }
}
