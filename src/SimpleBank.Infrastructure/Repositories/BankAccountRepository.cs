using Microsoft.EntityFrameworkCore;
using SimpleBank.Domain.BankAccountAggregate;
using SimpleBank.Domain.Contracts;
using System.Linq.Expressions;

namespace SimpleBank.Infrastructure.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly SimpleBankDbContext _dbContext;

        // TODO: Implement all BankAccountRepository Methods

        public IUnitOfWork UnitOfWork => _dbContext;

        public BankAccountRepository(SimpleBankDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<BankAccount?> GetByAccountNumberAndIFSC(string AccountNumber, string IFSC)
        {
            return await _dbContext.BankAccounts
                        .Include(a => a.TransactionRecords)
                        .Where(a => a.AccountNumber == AccountNumber)
                        .Where(a => a.BranchIFSC == IFSC)
                        .SingleOrDefaultAsync();
        }

        public async Task<BankAccount?> GetById(long id)
        {
            return await _dbContext.BankAccounts
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BankAccount>> List()
        {
            return await _dbContext.BankAccounts
                .ToListAsync();
        }

        public async Task<IEnumerable<BankAccount>> List(Expression<Func<BankAccount, bool>> predicate)
        {
            return await _dbContext.BankAccounts
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<BankAccount> Add(BankAccount entity)
        {
            var account = (await _dbContext.BankAccounts.AddAsync(entity)).Entity;
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task Delete(BankAccount entity)
        {
             _dbContext.BankAccounts.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(BankAccount entity)
        {
            _dbContext.BankAccounts.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
