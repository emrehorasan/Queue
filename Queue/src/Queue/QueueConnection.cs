using System;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Queue
{
    public class QueueConnection
    {
        private readonly string _hostName;
        private readonly string _user;
        private readonly string _password;

        public QueueConnection(IConfiguration configuration)
        {
            _hostName = configuration.GetSection("RabbitMq:Host").Value;
            _user = configuration.GetSection("RabbitMq:Username").Value;
            _password = configuration.GetSection("RabbitMq:Password").Value;
        }

        public IConnection GetDefaultConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _user,
                Password =_password,
                RequestedHeartbeat = 60,
                ContinuationTimeout = TimeSpan.FromSeconds(30)
            };

            return connectionFactory.CreateConnection();
        }
    }
}
