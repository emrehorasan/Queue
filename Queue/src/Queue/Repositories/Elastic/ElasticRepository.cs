using Queue.Entities;

namespace Queue.Repositories.Elastic
{
    public class ElasticRepository<TEntity> : ElasticRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        public ElasticRepository(ElasticDbContextBase contextBase) : base(contextBase)
        {
        }
    }
}
