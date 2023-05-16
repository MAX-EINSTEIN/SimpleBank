using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Shared;

namespace SimpleBank.Domain.BankAggregate
{
    public class Bank : Entity, IAggregateRoot
    {
        public string Name { get; }
        public Address Address { get;  }
       
        public string BranchIFSC { get; }

        public decimal TransactionLimit { get; }
        public string Currency { get; }

        private readonly List<BankAccount> _accounts = new();
        public IEnumerable<BankAccount> Accounts { get => _accounts.AsReadOnly(); } 

        public Bank()
        {

        }

        public Bank(string name, Address address, decimal transactionLimit, string currency)
        {
            Name = name;
            Address = address;
            BranchIFSC = AlphaNumericGenerator.GetRandomAlphaNumeric(3, 7);
            TransactionLimit = transactionLimit;
            Currency = currency;
        }

        
        public BankAccount CreateAccount(decimal initialBalance = 0m) 
        {
            var account = new BankAccount(
                AlphaNumericGenerator.GetRandomNumbers(10), 
                this,
                initialBalance
            );
            _accounts.Add(account);
            return account;
        }

        // [TODO]: Implement register customer methods and reference to it in BankAccount
    }
}