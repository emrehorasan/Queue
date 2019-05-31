using System;
using System.Threading.Tasks;
using Nest;
using Queue.Entities;

namespace Queue.Repositories.Elastic
{
    public abstract class ElasticRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ElasticClient _client;
        protected ElasticRepositoryBase(ElasticDbContextBase contextBase)
        {
            _client = contextBase.Client;
        }
        public Task Insert<TEntiy>(TEntity entity)
        {
            return _client.IndexDocumentAsync(entity);
        }

        public Task<TEntity> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
