using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Queue.Api.Models;
using Queue.Api.Repositories;
using Queue.Repositories;
using Queue.Repositories.Mongo;
using RabbitMQ.Client;

namespace Queue.Api.Services.Products
{
    public interface IProductService
    {
        void Create(Product model);
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> SearchAsync(string query);
    }
    public class ProductService : IProductService
    {
        private const string QueueName = "product";
        private const string ExChangeName = "product-exchange";
        private readonly QueueConnection _connection;
        private readonly ProductMongoRepository _productMongoRepository;
        private readonly ProductElasticRepository _productElasticRepository;

        public ProductService(QueueConnection connection,ProductMongoRepository productMongoRepository, ProductElasticRepository productElasticRepository)
        {
            _connection = connection;
            _productMongoRepository = productMongoRepository;
            _productElasticRepository = productElasticRepository;
        }

        public void Create(Product model)
        {
            var productString = JsonConvert.SerializeObject(model);

            using (var connection = _connection.GetDefaultConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(ExChangeName, ExchangeType.Fanout);

                    var body = Encoding.UTF8.GetBytes(productString);
                    channel.BasicPublish(ExChangeName,
                        routingKey: "",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine(" [x] Sent {0}", model);
                }
            }
        }

        public Task<Product> GetAsync(int id)
        {
            return _productMongoRepository.Get(id);
        }

        public Task<IEnumerable<Product>> SearchAsync(string query)
        {
            return _productElasticRepository.Search(query);
        }
    }
}
