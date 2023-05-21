using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Services
{

    public class BankBranchManagementService: IBankBranchManagementService {
        private readonly IBankRepository _bankRepository;
        private readonly IBankBranchRepository _bankBranchRepository;


        public BankBranchManagementService(IBankBranchRepository bankBranchRepository, IBankRepository bankRepository)
        {
            _bankBranchRepository = bankBranchRepository;
            _bankRepository = bankRepository;
        }


        public async Task<BankBranch?> AddBankBranch(long bankId, string name, Address address)
        {
            var bank = await _bankRepository.GetById(bankId) ?? throw new ArgumentException("Cannot add bank branch to a non-existent bank.");

            var branch = new BankBranch(bank, name, address);
            await _bankBranchRepository.Add(branch);

            return await _bankBranchRepository.GetById(branch.Id);
        }


        public async Task<bool> RemoveBankBranch(long bankId, long branchId)
        {
            var noOfBankBranches = await _bankBranchRepository.GetNumberOfBranchesByBankId(bankId);
            
            if (noOfBankBranches <= 1) return false;

            var branch = await _bankBranchRepository.GetById(branchId)
                ?? throw new ArgumentException("Bank Branch with the provided Branch Code doesn't exist");
            
            await _bankBranchRepository.Delete(branch);

            return true;
        }
    }

}
