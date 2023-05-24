using SimpleBank.Domain.BankAccountAggregate;
using SimpleBank.Domain.Common;

namespace SimpleBanks.Tests
{
    public class BankAccountAggregateUnitTests
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
        public void BankAccountIsCreatedCorrectly()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);
            
            Assert.NotNull(account);
            Assert.True(account.TransactionRecords.Count() == 0);
        }

        [Fact]
        public void BankAccountDepositUpdatesBalance()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);

            var amountToDeposit = 10_000m;
            account.DepositAmount(amountToDeposit);

            Assert.Equal(amountToDeposit, account.Balance);
            Assert.True(account.TransactionRecords.Count() == 1);
        }

        [Fact]
        public void BankAccountDepositFailsOnNegativeOrZeroAmount()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);

            var negativeAmount = -10_000m;
            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(negativeAmount));

            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(decimal.Zero));
        }

        [Fact]
        public void BankAccountDepositFailsOnDepostingAmountGreaterThanTransactionLimit()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);

            var largeAmount = 5_000_000m;
            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(largeAmount));
        }

        [Fact]
        public void BankAccountWithdrawalUpdatesBalance()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);
            
            account.DepositAmount(10_000m);

            account.WithdrawAmount(10_000m);

            Assert.Equal(decimal.Zero, account.Balance);
            Assert.True(account.TransactionRecords.Count() == 2);
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnNegativeOrZeroAmount()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);

            var negativeAmount = -10_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(negativeAmount));

            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(decimal.Zero));
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnDepostingAmountGreaterThanTransactionLimit()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);

            var largeAmount = 2_000_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(largeAmount));
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnWithdrawingAmountGreaterThanBalance()
        {
            var account = new BankAccount("2017559065", "HDFC0204608", _customer);

            var exceedingAmount = 600_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(exceedingAmount));
        }
    }
}