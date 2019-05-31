using Queue.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Queue.Api.Services.Products
{
    public interface IProductService
    {
        void Create(Product model);
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> SearchAsync(string query);
    }
}
