using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Contracts
{
    public interface IBankBranchManagementService
    {
        public Task<BankBranch?> AddBankBranch(long bankId, string name, Address address);
        public Task<bool> RemoveBankBranch(long bankId, long branchId);
    }
}
