using System.Linq.Expressions;

namespace SimpleBank.Domain.Contracts
{
    public interface IRepository<T> where T: Entity, IAggregateRoot
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
