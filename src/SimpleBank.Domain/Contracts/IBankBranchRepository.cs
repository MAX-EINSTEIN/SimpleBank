using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Contracts
{
    public interface IBankBranchRepository : IRepository<BankBranch>
    {
        public Task<int> GetNumberOfBranchesByBankId(long bankId);
        public Task<BankBranch?> GetByBranchCode(string branchCode);
    }
}
