using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.BankBranchAggregate
{
    public interface IBankBranchRepository : IRepository<BankBranch>
    {
        public Task<int> GetNumberOfBranchesByBankId(long bankId);
        public Task<BankBranch?> GetByBranchCode(string branchCode);
    }
}
