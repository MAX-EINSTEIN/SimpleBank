using SimpleBank.Domain.Base;

namespace SimpleBank.Domain.SharedKernel
{
    public class Address: ValueObject
    {
        public string Building { get; }
        public string Street { get; }
        public string Block { get; }
        public string City { get; }
        public string Region { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public Address(string building, string street, string block, string city, string region, string country, string zipcode)
        {
            Building = building;
            Street = street;
            Block = block;
            City = city;
            Region = region;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Building; 
            yield return Street; 
            yield return Block; 
            yield return City; 
            yield return Region; 
            yield return Country; 
            yield return ZipCode;
        }
    }
}
