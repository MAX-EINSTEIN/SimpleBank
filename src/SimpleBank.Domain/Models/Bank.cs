using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Utils;

namespace SimpleBank.Domain.Models
{
    public class Bank : Entity, IAggregateRoot
    {
        public string Name { get; }
        public Address Address { get; }

        public string BranchIFSC { get; }

        private readonly List<BankAccount> _accounts = new();
        public IList<BankAccount> Accounts { get => _accounts.AsReadOnly(); }

        public Bank()
        {
            
        }

        public Bank(string name,
                    Address address,
                    string bankCode = "SBIN",
                    string branchCode = "005943")
        {
            Name = name;
            Address = address;
            BranchIFSC = bankCode + "0" + branchCode;
        }


        public BankAccount CreateAccount(Customer accountHolder,
                                         decimal transactionLimit,
                                         string currency)
        {
            var account = new BankAccount(
                AlphaNumericGenerator.GetRandomNumbers(10),
                BranchIFSC,
                accountHolder,
                transactionLimit,
                currency
            );
            _accounts.Add(account);
            return account;
        }

        public bool DeleteAccount(long accountId)
        {
            var account = Accounts.Where(a => a.Id == accountId).FirstOrDefault() ;
            
            if (account is null) { return false; }

            return _accounts.Remove(account);
        }

    }
}