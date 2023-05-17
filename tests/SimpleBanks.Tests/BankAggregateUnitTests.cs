using SimpleBank.Domain.Models;

namespace SimpleBanks.Tests
{
    public class BankAggregateUnitTests
    {
        private readonly Address _address = new (
                "2436 Hamilton Drive",
                "East Baby",
                "New Mexico",
                "USA",
                "04860"
            );

        private readonly Customer _customer = new Customer(
                "Barry Allen",
                "Male",
                "barry.allen@gmail.com",
                "9798767890",
                new(
                    "2436 Hamilton Drive",
                    "East Baby",
                    "New Mexico",
                    "USA",
                    "04860"
                )
            );

        [Fact]
        public void BranchIFSCIsGeneratedCorrectly()
        {
            var bank = new Bank("HDFC", _address);

            Assert.NotNull(bank.BranchIFSC);
            Assert.NotEmpty(bank.BranchIFSC);
            Assert.True(bank.BranchIFSC.Substring(0, 3).All(char.IsLetter));
            Assert.True(bank.BranchIFSC.Substring(3, 7).All(char.IsNumber));
        }

        [Fact]
        public void BankAccountIsCreatedAndAddedCorrectly()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1_000_000m, "INR");

            Assert.NotNull(account);
            Assert.NotNull(bank.Accounts);
            Assert.Contains(account, bank.Accounts);
            Assert.True(account.AccountNumber.Length == 10);
            Assert.True(account.AccountNumber.All(char.IsNumber));
        }

        [Fact]
        public void BankAccountDepositUpdatesBalance()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1000000m, "INR");

            var amountToDeposit = 10_000m;
            account.DepositAmount(amountToDeposit);

            Assert.Equal(amountToDeposit, account.Balance);
        }

        [Fact]
        public void BankAccountDepositFailsOnNegativeOrZeroAmount()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1_000_000m, "INR");

            var negativeAmount = -10_000m;
            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(negativeAmount));

            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(decimal.Zero));
        }

        [Fact]
        public void BankAccountDepositFailsOnDepostingAmountGreaterThanTransactionLimit()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1000000m, "INR");

            var largeAmount = 5_000_000m;
            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(largeAmount));
        }

        [Fact]
        public void BankAccountWithdrawalUpdatesBalance()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1000000m, "INR");
            account.DepositAmount(10_000m);

            account.WithdrawAmount(10_000m);

            Assert.Equal(decimal.Zero, account.Balance);
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnNegativeOrZeroAmount()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1000000m, "INR");

            var negativeAmount = -10_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(negativeAmount));

            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(decimal.Zero));
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnDepostingAmountGreaterThanTransactionLimit()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1_000_000m, "INR");

            var largeAmount = 2_000_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(largeAmount));
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnWithdrawingAmountGreaterThanBalance()
        {
            var bank = new Bank("HDFC", _address);

            var account = bank.CreateAccount(_customer, 1000000m, "INR");

            var exceedingAmount = 600_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(exceedingAmount));
        }
    }
}