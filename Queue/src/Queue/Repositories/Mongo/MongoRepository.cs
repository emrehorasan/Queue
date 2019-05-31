using Queue.Entities;

namespace Queue.Repositories.Mongo
{
    public class MongoRepository<TEntity> : MongoRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        public MongoRepository(MongoDbContext context) : base(context)
        {
        }
    }
}
