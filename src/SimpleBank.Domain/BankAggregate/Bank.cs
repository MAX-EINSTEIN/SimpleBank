using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Common;

namespace SimpleBank.Domain.BankAggregate
{
    public class Bank : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string BankCode { get; }

        public Bank()
        {

        }

        public Bank(string name, string bankCode = "SBIN")
        {
            Name = name;
            BankCode = bankCode;
        }
    }
}