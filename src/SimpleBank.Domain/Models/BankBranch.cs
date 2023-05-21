using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Utils;

namespace SimpleBank.Domain.Models
{
    public class BankBranch: Entity, IAggregateRoot
    {
        private readonly long _bankId;
        public long BankId => _bankId;

        public string Name { get;  }
        public string BranchCode { get; }
        public Address Address { get; }

        public BankBranch() { }

        public BankBranch(Bank bank,
                          string name,
                          Address address)
        {
            _bankId = bank?.Id ?? throw new ArgumentNullException(nameof(bank)); ;
            Name = name;
            BranchCode = AlphaNumericGenerator.GetRandomNumbers(6);
            Address = address;
        }
    }
}
