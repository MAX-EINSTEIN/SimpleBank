using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Contracts
{
    public interface IBankBranchManagementService
    {
        public Task<BankBranch?> AddBankBranch(string bankCode, string name, Address address);
        public Task<bool> RemoveBankBranch(string bankCode, string branchCode);
    }
}
