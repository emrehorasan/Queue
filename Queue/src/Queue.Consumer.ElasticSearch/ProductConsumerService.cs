using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Queue.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Queue.Consumer.ElasticSearch
{
    public class ProductConsumerService : BackgroundService
    {
        private readonly QueueConnection _queueConnection;
        private readonly IRepository<Product> _productRepository;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private IConnection _connection;
        private string _queueName;

        public ProductConsumerService(QueueConnection queueConnection, IRepository<Product> productRepository)
        {
            _queueConnection = queueConnection;
            _productRepository = productRepository;
            Initialization();
        }
        private void Initialization()
        {
            _connection = _queueConnection.GetDefaultConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("product-exchange", ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "product-exchange", routingKey: "");

            Console.WriteLine(" [*] Elastic waiting for product.");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                _productRepository.Insert<Product>(JsonConvert.DeserializeObject<Product>(message));
                Console.WriteLine(" [x] {0}", message);
            };

            _channel.BasicConsume(queue: _queueName,autoAck: true, consumer: _consumer);
            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
