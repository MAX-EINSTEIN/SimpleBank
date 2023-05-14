using SimpleBank.Domain.Base;
using SimpleBank.Domain.Shared;
using SimpleBank.Domain.SharedKernel;
using System.Security.Cryptography.X509Certificates;

namespace SimpleBank.Domain.BankAggregate
{
    public class Bank : Entity, IAggregateRoot
    {
        public string Name { get; }
        public Address Address { get; }
       
        // [TODO]: Assign IFSC based on bank branch in the future
        public string BankBranchIFSC { get; } = string.Empty;

        public decimal TransactionLimit { get; }
        public string Currency { get; }

        private readonly List<BankAccount> _accounts = new();
        public IEnumerable<BankAccount> Accounts { get => _accounts.AsReadOnly(); } 

        public Bank(string name, Address address, string branchIFSC, decimal transactionLimit, string currency)
        {
            Name = name;
            Address = address;
            BankBranchIFSC = branchIFSC != string.Empty 
                ? branchIFSC 
                : AlphaNumericGenerator.GetRandomAlphaNumeric(3, 7);
            TransactionLimit = transactionLimit;
            Currency = currency;
        }

        
        public BankAccount CreateAccount() 
        {
            var account = new BankAccount(
                AlphaNumericGenerator.GetRandomNumbers(10), 
                this
            );
            _accounts.Add(account);
            return account;
        }

        // [TODO]: Implement register customer methods and reference to it in BankAccount
    }
}