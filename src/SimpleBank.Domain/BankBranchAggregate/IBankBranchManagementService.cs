using SimpleBank.Domain.Common;

namespace SimpleBank.Domain.BankBranchAggregate
{
    public interface IBankBranchManagementService
    {
        public Task<BankBranch?> GetABranchOfABank(string bankCode, string branchCode);
        public Task<IEnumerable<BankBranch>> GetAllBranchesOfABank(string bankCode);
        public Task<BankBranch?> AddBankBranch(string bankCode, string name, Address address);
        public Task<bool> RemoveBankBranch(string bankCode, string branchCode);
    }
}
