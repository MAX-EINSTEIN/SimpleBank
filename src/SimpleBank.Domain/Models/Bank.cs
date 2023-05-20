using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Utils;

namespace SimpleBank.Domain.Models
{
    public class Bank : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string BankCode { get; }

        private readonly List<BankAccount> _accounts = new();
        public IList<BankAccount> Accounts { get => _accounts.AsReadOnly(); }

        public Bank()
        {
            
        }

        public Bank(string name, string bankCode = "SBIN")
        {
            Name = name;
            BankCode = bankCode;
        }


        public BankAccount CreateAccount(Customer accountHolder,
                                         decimal transactionLimit,
                                         string currency)
        {
            var BranchIFSC = BankCode + "0" + AlphaNumericGenerator.GetRandomNumbers(6);

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