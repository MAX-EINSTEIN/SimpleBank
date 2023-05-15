using Microsoft.EntityFrameworkCore;
using SimpleBank.Domain.Contracts;
using System.Linq.Expressions;

namespace SimpleBank.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : Entity, IAggregateRoot
    {
        private readonly SimpleBankDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public Repository(SimpleBankDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            await _dbContext.Set<T>().Remove<T>(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}