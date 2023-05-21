using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;
using System.Linq.Expressions;

namespace SimpleBank.Infrastructure.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly SimpleBankDbContext _dbContext;

        // TODO: Implement all BankAccountRepository Methods

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Task<BankAccount> GetByAccountNumberAndIFSC(string AccountNumber, string IFSC)
        {
            throw new NotImplementedException();
        }

        public Task<BankAccount?> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BankAccount>> List()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BankAccount>> List(Expression<Func<BankAccount, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<BankAccount> Add(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(BankAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
