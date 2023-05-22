using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;
using SimpleBank.Domain.Utils;


namespace SimpleBank.Domain.Services
{
    public class BankAccountManagementDomainService: IBankAccountManagementDomainService
    {
        private readonly IBankAccountRepository _bankAccountRepository;


        public BankAccountManagementDomainService(IBankAccountRepository bankAccountRepository) { 
            _bankAccountRepository = bankAccountRepository;
        }


        public async Task<BankAccount> CreateAccount(Customer accountHolder,
                                         decimal transactionLimit,
                                         string currency,
                                         string branchCode,
                                         string bankCode)
        {
            var BranchIFSC = bankCode + "0" + branchCode;

            var account = new BankAccount(
                AlphaNumericGenerator.GetRandomNumbers(16),
                BranchIFSC,
                accountHolder,
                transactionLimit,
                currency
            );

            await _bankAccountRepository.Add(account);

            return account;
        }


        public async Task<bool> DeleteAccount(string AccountNumber, string IFSC)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(AccountNumber, IFSC); 

            if (account is null) { return false;  }

            await _bankAccountRepository.Delete(account);

            return true;
        }

    }
}
