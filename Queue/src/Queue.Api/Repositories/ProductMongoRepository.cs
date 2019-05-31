using Queue.Api.Models;
using Queue.Repositories.Mongo;

namespace Queue.Api.Repositories
{
    public class ProductMongoRepository : MongoRepository<Product>
    {
        public ProductMongoRepository(MongoDbContext context) : base(context)
        {
        }
    }
}
