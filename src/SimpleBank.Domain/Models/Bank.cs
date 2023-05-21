using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Utils;

namespace SimpleBank.Domain.Models
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