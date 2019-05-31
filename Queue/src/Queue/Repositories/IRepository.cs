using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Repositories
{
    public interface IRepository
    {

    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        Task Insert<TEntiy>(TEntity entity);
        Task<TEntity> Get(int id);
    }
}
