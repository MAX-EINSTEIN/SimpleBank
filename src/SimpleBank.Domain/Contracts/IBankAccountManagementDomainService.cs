using SimpleBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Domain.Contracts
{
    public interface IBankAccountManagementDomainService
    {
        public Task<BankAccount> CreateAccount(Customer accountHolder,
                                               decimal transactionLimit,
                                               string currency,
                                               string branchCode,
                                               string bankCode);
        public Task<bool> DeleteAccount(string AccountNumber, string IFSC);
    }
}
