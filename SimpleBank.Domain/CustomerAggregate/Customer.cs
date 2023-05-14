using SimpleBank.Domain.Base;
using SimpleBank.Domain.SharedKernel;

namespace SimpleBank.Domain.CustomerAggregate
{
    public class Customer: Entity
    {
        public string Name { get; }
        public string Gender { get; } 
        public string Email { get; }
        public string PhoneNumber { get; }
        public Address Address { get; }

        public Customer(string name, string gender, string email, string phoneNumber, Address address)
        {
            Name = name;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
