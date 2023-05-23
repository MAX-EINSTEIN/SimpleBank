using Microsoft.EntityFrameworkCore;
using SimpleBank.Domain.BankBranchAggregate;
using SimpleBank.Domain.Contracts;
using System.Linq.Expressions;

namespace SimpleBank.Infrastructure.Repositories
{
    public class BankBranchRepository : IBankBranchRepository
    {
        private readonly SimpleBankDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public BankBranchRepository(SimpleBankDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<BankBranch?> GetById(long id)
        {
            return await _dbContext.BankBranches.Where(b => b.Id == id).SingleOrDefaultAsync();
        }

        public async Task<int> GetNumberOfBranchesByBankId(long bankId)
        {
            return await _dbContext.BankBranches.Where(b => b.BankId == bankId).CountAsync();
        }

        public async Task<BankBranch?> GetByBranchCode(string branchCode)
        {
            return await _dbContext.BankBranches.Where(b => b.BranchCode == branchCode).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BankBranch>> List()
        {
            return await _dbContext.BankBranches.ToListAsync();
        }

        public async Task<IEnumerable<BankBranch>> List(Expression<Func<BankBranch, bool>> predicate)
        {
            return await _dbContext.BankBranches.Where(predicate).ToListAsync();
        }

        public async Task<BankBranch> Add(BankBranch entity)
        {
            var bankBranch = (await _dbContext.BankBranches.AddAsync(entity)).Entity;
            await _dbContext.SaveChangesAsync();

            return bankBranch;
        }

        public async Task Delete(BankBranch entity)
        {
            _dbContext.BankBranches.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }


        public async Task Update(BankBranch entity)
        {
            _dbContext.BankBranches.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
