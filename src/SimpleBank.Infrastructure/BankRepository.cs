using Microsoft.EntityFrameworkCore;
using SimpleBank.Domain.BankAggregate;
using SimpleBank.Domain.Contracts;
using System.Linq.Expressions;

namespace SimpleBank.Infrastructure
{
    public class BankRepository: IBankRepository
    {
        private readonly SimpleBankDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public BankRepository(SimpleBankDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Bank?> GetById(long id)
        {
            return await _dbContext.Banks
                .Include(b => b.Accounts)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Bank?> GetByName(string bankName)
        {
            return await _dbContext.Banks
                .Include(b => b.Accounts)
                .Where(b => b.Name == bankName)
                .SingleOrDefaultAsync();
        }

        public async Task<Bank?> GetByIFSC(string branchIFSC)
        {
            return await _dbContext.Banks
                .Include(b => b.Accounts)
                .Where(b => b.BranchIFSC == branchIFSC)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Bank>> List()
        {
            return await _dbContext.Banks
                .Include(b => b.Accounts)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bank>> List(Expression<Func<Bank, bool>> predicate)
        {
            return await _dbContext.Banks
                .Include(b => b.Accounts)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Bank> Add(Bank entity)
        {
            var bank = (await _dbContext.Banks.AddAsync(entity)).Entity;
            await _dbContext.SaveChangesAsync();

            return bank;
        }

        public async Task Delete(Bank entity)
        {
            _dbContext.Banks.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Bank entity)
        {
            _dbContext.Banks.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
