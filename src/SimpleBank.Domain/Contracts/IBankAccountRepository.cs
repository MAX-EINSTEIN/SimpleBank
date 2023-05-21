using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Contracts
{
    public interface IBankAccountRepository: IRepository<BankAccount>
    {
        public Task<BankAccount> GetByAccountNumberAndIFSC(string AccountNumber, string IFSC);
    }
}
