using SimpleBank.Domain.Base;

namespace SimpleBank.Domain.SharedKernel
{
    public class Address: ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string Region { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string region, string country, string zipcode)
        {
            Street = street;
            City = city;
            Region = region;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street; 
            yield return City; 
            yield return Region; 
            yield return Country; 
            yield return ZipCode;
        }

        public override string ToString()
            => $"{Street}\n"
            +  $"{City}, {Region}\n" 
            +  $"{Country} - {ZipCode}";
    }
}
