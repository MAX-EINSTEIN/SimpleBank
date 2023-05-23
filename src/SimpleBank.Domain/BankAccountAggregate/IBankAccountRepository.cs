using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.BankAccountAggregate
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        public Task<BankAccount?> GetByAccountNumberAndIFSC(string AccountNumber, string IFSC);
    }
}
