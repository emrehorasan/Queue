using System.Threading.Tasks;
using Queue.Events;
using Queue.Repositories;

namespace Queue.Consumer.ElasticSearch.Products.Events
{
    public class ProductAddedEventHandler : IEventHandler<ProductAddedEvent>
    {
        private readonly IRepository<Product> _productRepository;

        public ProductAddedEventHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductAddedEvent @event)
        {
            await _productRepository.Insert<Product>(@event.Product);
        }
    }
}
