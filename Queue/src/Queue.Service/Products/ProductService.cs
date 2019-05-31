using RabbitMQ.Client;
using System;
using System.Text;

namespace Queue.Service.Products
{
    public interface IProductService
    {
        void Create(string message);
    }
    public class ProductService : IProductService
    {
        private const string QueueName = "Product";
        private readonly QueueConnection _connection;

        public ProductService(QueueConnection connection)
        {
            _connection = connection;
        }

        public void Create(string message)
        {
            using (var connection = _connection.GetDefaultConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", ExchangeType.Fanout);

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "logs",
                        routingKey: "",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
