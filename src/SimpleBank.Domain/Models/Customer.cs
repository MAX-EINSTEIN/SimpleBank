using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.Models
{
    public class Customer : ValueObject
    {
        public string Name { get; }
        public string Gender { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public Address Address { get; }

        public Customer()
        {

        }

        public Customer(string name, string gender, string email, string phoneNumber, Address address)
        {
            Name = name;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Gender;
            yield return Email;
            yield return PhoneNumber;
            yield return Address;
        }
    }
}
