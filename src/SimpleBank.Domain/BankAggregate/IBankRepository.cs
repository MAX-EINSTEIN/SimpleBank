using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.BankAggregate
{
    public interface IBankRepository : IRepository<Bank>
    {
        public Task<Bank?> GetByName(string bankName);
        public Task<Bank?> GetByBankCode(string bankCode, bool fetchTransactionRecords = false);
    }
}
