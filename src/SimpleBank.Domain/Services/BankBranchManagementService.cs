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


        public async Task<BankBranch?> AddBankBranch(string bankCode, string name, Address address)
        {
            var bank = await _bankRepository.GetByBankCode(bankCode) ?? throw new ArgumentException("Cannot add bank branch to a non-existent bank.");

            var branch = new BankBranch(bank, name, address);
            await _bankBranchRepository.Add(branch);

            return await _bankBranchRepository.GetById(branch.Id);
        }


        public async Task<bool> RemoveBankBranch(string bankCode, string branchCode)
        {
            var bank = await _bankRepository.GetByBankCode(bankCode) ?? throw new ArgumentException("Cannot add bank branch to a non-existent bank.");

            var noOfBankBranches = await _bankBranchRepository.GetNumberOfBranchesByBankId(bank.Id);
            
            if (noOfBankBranches <= 1) return false;

            var branch = await _bankBranchRepository.GetByBranchCode(branchCode)
                ?? throw new ArgumentException("Bank Branch with the provided Branch Code doesn't exist");
            
            await _bankBranchRepository.Delete(branch);

            return true;
        }
    }

}
