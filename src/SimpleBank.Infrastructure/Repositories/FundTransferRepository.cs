using Microsoft.EntityFrameworkCore;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.FundTransferAggregate;
using System.Linq.Expressions;

namespace SimpleBank.Infrastructure.Repositories
{
    public class FundTransferRepository : IFundTransferRepository
    {
        private readonly SimpleBankDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public FundTransferRepository(SimpleBankDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }


        public async Task<FundTransfer?> GetById(long id)
        {
            return await _dbContext.FundTransfers
                            .Where(t => t.Id == id)
                            .SingleOrDefaultAsync();
        }


        public async Task<FundTransfer?> GetByUTRNumber(string UTRNumber)
        {
            return await _dbContext.FundTransfers
                            .Where(t => t.BankReferenceNo == UTRNumber)
                            .SingleOrDefaultAsync();
        }


        public async Task<IEnumerable<FundTransfer>> List()
        {
            return await _dbContext.FundTransfers.ToListAsync();
        }


        public async Task<IEnumerable<FundTransfer>> List(Expression<Func<FundTransfer, bool>> predicate)
        {
            return await _dbContext.FundTransfers
                            .Where(predicate)
                            .ToListAsync();
        }


        public async Task<FundTransfer> Add(FundTransfer entity)
        {
            var fundTransfer = (await _dbContext.FundTransfers.AddAsync(entity)).Entity;
            await _dbContext.SaveChangesAsync();
            return fundTransfer;
        }


        public async Task Delete(FundTransfer entity)
        {
            _dbContext.FundTransfers.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }


        public async Task Update(FundTransfer entity)
        {
            _dbContext.FundTransfers.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
