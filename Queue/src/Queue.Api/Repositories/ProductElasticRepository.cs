using System.Collections.Generic;
using System.Threading.Tasks;
using Queue.Api.Models;
using Queue.Repositories.Elastic;

namespace Queue.Api.Repositories
{
    public class ProductElasticRepository : ElasticRepository<Product>, ISearchRepository<Product>
    {
        private readonly ElasticDbContext _contextBase;

        public ProductElasticRepository(ElasticDbContext contextBase) : base(contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<IEnumerable<Product>> Search(string query)
        {
            var result = await _contextBase.Client.SearchAsync<Product>(s => s.Query(q => q.QueryString(d => d.Query(query))));
            return result.Documents;
        }
    }
}
