using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.Models
{
    public class BankAccount : Entity
    {
        public string AccountNumber { get; }

        public string BranchIFSC { get; }

        public decimal TransactionLimit { get; }

        public string Currency { get; }

        private decimal _balance = decimal.Zero;
        public decimal Balance => Math.Round(_balance, 2);

        public Customer AccountHolder { get; }

        public BankAccount()
        {

        }

        public BankAccount(string accountNumber,
                           string branchIFSC,
                           Customer accountHolder,
                           decimal transactionLimit = 100_000m,
                           string currency = "INR")
        {
            AccountNumber = accountNumber;
            BranchIFSC = branchIFSC;
            AccountHolder = accountHolder;
            _balance = 0m;
            TransactionLimit = transactionLimit;
            Currency = currency;
        }

        private void UpdateBalance(decimal updatedBalance)
        {
            if (updatedBalance < 0m)
                throw new InvalidOperationException($"Balance in account can not be less than {Currency} 0.00");

            _balance = updatedBalance;
        }

        public void DepositAmount(decimal amount)
        {
            if (amount <= 0m) throw new InvalidOperationException($"Amount to deposit can not be less than {Currency} 0.00");
            if (amount <= TransactionLimit)
            {
                UpdateBalance(_balance + amount);
            }
            else
            {
                throw new InvalidOperationException($"Amount exceeds the transaction limit of {Currency} {TransactionLimit}");
            }
        }

        public void WithdrawAmount(decimal amount)
        {
            if (amount <= 0m) throw new InvalidOperationException($"Amount to deposit can not be less than {Currency} 0.00");
            if (amount <= TransactionLimit)
            {
                try
                {
                    UpdateBalance(Balance - amount);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Not enough funds to withdraw the request amount\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Some unspecified error occured. Please contact the bank for further details\n" + ex.Message);
                }
            }
            else
            {
                throw new InvalidOperationException($"Amount exceeds the transaction limit of {Currency} {TransactionLimit}");
            }
        }
    }
}
