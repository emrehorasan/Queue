using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Queue.Api.Models;
using Queue.Repositories;
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
