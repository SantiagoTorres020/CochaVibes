using CochaVibes.Core.Entities;
using System.Linq.Expressions;

namespace CochaVibes.Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes);

        Task Add(T entity);

        void Update(T entity);

        Task Delete(int id);
    }
}