using Queue.Api.Models;
using Queue.Api.Repositories;
using Queue.Api.Services.Products.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using Queue.Events;

namespace Queue.Api.Services.Products
{
    public class ProductService : IProductService
    {
        #region .ctor

        private readonly ProductMongoRepository _productMongoRepository;
        private readonly ProductElasticRepository _productElasticRepository;
        private readonly IEventBus _eventBus;

        public ProductService(ProductMongoRepository productMongoRepository, ProductElasticRepository productElasticRepository, IEventBus eventBus)
        {
            _productMongoRepository = productMongoRepository;
            _productElasticRepository = productElasticRepository;
            _eventBus = eventBus;
        }
        

        #endregion
        public void Create(Product model)
        {
            _eventBus.Publish(new ProductAddedEvent{Product = model}); 
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
