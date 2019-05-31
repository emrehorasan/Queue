using System;
using RabbitMQ.Client;

namespace Queue.Service
{
    public class QueueConnection
    {
        private readonly string _hostName = "localhost";

        public IConnection GetDefaultConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };

            return connectionFactory.CreateConnection();
        }
    }
}
