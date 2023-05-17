using SimpleBank.Domain.Models;

namespace SimpleBank.Tests
{
    public class AddressUnitTests
    {
        [Fact]
        public void AddressIsConvertedToStringCorrectly()
        {
            var address = new Address(
                "2436 Hamilton Drive",
                "East Baby",
                "New Mexico",
                "USA",
                "04860"
            );

            var resultString = address.ToString();
            var expectedString = "2436 Hamilton Drive\n"
                + "East Baby, New Mexico\n"
                + "USA - 04860";

            Assert.Equal(resultString, expectedString);
        }

        [Fact]
        public void TwoAddressesAreEqualIfAllFieldsHaveSameValue()
        {
            var addressA = new Address(
                "2436 Hamilton Drive",
                "East Baby",
                "New Mexico",
                "USA",
                "04860"
            );

            var addressB = new Address(
                "2436 Hamilton Drive",
                "East Baby",
                "New Mexico",
                "USA",
                "04860"
            );

            Assert.Equal(addressA, addressB);
        }

        [Fact]
        public void TwoAddressesAreNotEqualIfAnyFieldHasADifferentValue()
        {
            var addressA = new Address(
                "2436 Hamilton Drive",
                "East Baby",
                "New Mexico",
                "USA",
                "04860"
            );

            var addressB = new Address(
                "300 Carolylins Circle",
                "East Baby",
                "New Mexico",
                "USA",
                "04860"
            );

            Assert.NotEqual(addressA, addressB);
        }
    }
}
