using System.Linq.Expressions;

namespace SimpleBank.Domain.Contracts
{
    public interface IRepository<T> where T: Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T?> GetById(long id);
        Task<IEnumerable<T>> List();
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}
