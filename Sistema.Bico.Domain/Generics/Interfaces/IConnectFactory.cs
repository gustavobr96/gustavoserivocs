using RabbitMQ.Client;

namespace Sistema.Bico.Domain.Generics.Interfaces
{
    public interface IConnectFactory
    {
        ConnectionFactory Get();
    }
}
