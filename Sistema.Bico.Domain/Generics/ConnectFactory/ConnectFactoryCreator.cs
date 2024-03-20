using Microsoft.AspNetCore.Connections;
using Sistema.Bico.Domain.Generics.Interfaces;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace Sistema.Bico.Domain.Generics.ConnectFactory
{
    public class ConnectFactoryCreator : IConnectFactory
    {
        private readonly IConfiguration _configuration;
        public ConnectFactoryCreator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConnectionFactory Get()
        {
            return new ConnectionFactory
            {
                //HostName = _configuration.GetConnectionString("RabbitConnection:HostName"),
                //Port = int.Parse(_configuration.GetConnectionString("RabbitConnection:Port")),
                //UserName = _configuration.GetConnectionString("RabbitConnection:UserName"),
                //Password = _configuration.GetConnectionString("RabbitConnection:Password"),
                //VirtualHost = _configuration.GetConnectionString("RabbitConnection:VirtualHost")

                HostName = _configuration.GetConnectionString("RabbitConnection")
            };
        }
    }
}
