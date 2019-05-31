using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Queue.Entities;

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
            try
            {
                return Collection.InsertOneAsync(entity);

            }
            catch (Exception ex)
            {
                //todo: handle exception
                throw ex;
            }
        }

        public async Task<TEntity> Get(int id)
        {
            var entity =  await Collection.FindAsync(x => x.Id == id);
            return entity.First();
        }
    }
}
