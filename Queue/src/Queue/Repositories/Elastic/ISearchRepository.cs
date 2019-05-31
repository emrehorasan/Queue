using System.Collections.Generic;
using System.Threading.Tasks;
using Queue.Entities;

namespace Queue.Repositories.Elastic
{
    public interface ISearchRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> Search(string query);
    }
}
