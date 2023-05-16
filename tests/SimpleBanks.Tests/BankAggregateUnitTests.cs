using SimpleBank.Domain.BankAggregate;
using SimpleBank.Domain.Shared;

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

        [Fact]
        public void BranchIFSCIsGeneratedCorrectly()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");
            
            Assert.NotNull(bank.BranchIFSC);
            Assert.NotEmpty(bank.BranchIFSC);
            Assert.True(bank.BranchIFSC.Substring(0, 3).All(char.IsLetter));
            Assert.True(bank.BranchIFSC.Substring(3, 7).All(char.IsNumber));
        }

        [Fact]
        public void BankAccountIsCreatedAndAddedCorrectly()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");
            
            var account = bank.CreateAccount();

            Assert.NotNull(account);
            Assert.NotNull(bank.Accounts);
            Assert.Contains(account, bank.Accounts);
            Assert.True(account.AccountNumber.Length == 10);
            Assert.True(account.AccountNumber.All(char.IsNumber));  
        }

        [Fact]
        public void BankAccountDepositUpdatesBalance()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount();

            var amountToDeposit = 10_000m;
            account.DepositAmount(amountToDeposit);

            Assert.Equal(amountToDeposit, account.Balance);
        }

        [Fact]
        public void BankAccountDepositFailsOnNegativeOrZeroAmount()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount();

            var negativeAmount = -10_000m;
            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(negativeAmount));

            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(decimal.Zero));
        }

        [Fact]
        public void BankAccountDepositFailsOnDepostingAmountGreaterThanTransactionLimit()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount();
            
            var largeAmount = 5_000_000m;
            Assert.Throws<InvalidOperationException>(() => account.DepositAmount(largeAmount));
        }

        [Fact]
        public void BankAccountWithdrawalUpdatesBalance()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount(10_000m);
            account.WithdrawAmount(10_000m);

            Assert.Equal(decimal.Zero, account.Balance);
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnNegativeOrZeroAmount()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount(5_00_000m);

            var negativeAmount = -10_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(negativeAmount));

            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(decimal.Zero));
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnDepostingAmountGreaterThanTransactionLimit()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount(5_000_000m);

            var largeAmount = 2_000_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(largeAmount));
        }

        [Fact]
        public void BankAccountWithdrawalFailsOnWithdrawingAmountGreaterThanBalance()
        {
            var bank = new Bank("HDFC", _address, 1_000_000m, "USD");

            var account = bank.CreateAccount(500_000m);

            var exceedingAmount = 600_000m;
            Assert.Throws<InvalidOperationException>(() => account.WithdrawAmount(exceedingAmount));
        }
    }
}