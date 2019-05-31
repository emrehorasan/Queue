using MongoDB.Driver;
using Queue.Entities;
using System.Threading.Tasks;

namespace Queue.Repositories.Mongo
{
    public abstract class MongoRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected IMongoCollection<TEntity> Collection { get; }

        protected MongoRepositoryBase(MongoDbContext context)
        {
            Collection = context.GetDatabase().GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
        }

        public Task Insert<TEntiy>(TEntity entity)
        {
           return Collection.InsertOneAsync(entity);
        }

        public async Task<TEntity> Get(int id)
        {
            var entity =  await Collection.FindAsync(x => x.Id == id);
            return entity.First();
        }
    }
}
