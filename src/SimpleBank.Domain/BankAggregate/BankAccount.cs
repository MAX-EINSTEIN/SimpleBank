using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.BankAggregate
{
    public class BankAccount: Entity
    {
        public string AccountNumber { get; }
        internal Bank Bank { get; }

        public string BranchIFSC { get => Bank.BranchIFSC; }

        private decimal _balance = decimal.Zero;
        public decimal Balance => Math.Round(_balance, 2);

        public BankAccount()
        {

        }

        public BankAccount(string accountNumber, Bank bank, decimal balance = 0m)
        {
            AccountNumber = accountNumber;
            Bank = bank;
            _balance = balance;
        }

        private void UpdateBalance(decimal updatedBalance) {
            if (updatedBalance < 0m)
                throw new InvalidOperationException($"Balance in account can not be less than {Bank.Currency} 0.00");

            _balance = updatedBalance;
        }
        
        public void DepositAmount(decimal amount)
        {
            if (amount <= 0m) throw new InvalidOperationException($"Amount to deposit can not be less than {Bank.Currency} 0.00");
            if(amount <= Bank.TransactionLimit)
            {
                UpdateBalance(_balance + amount);
            }
            else
            {
                throw new InvalidOperationException($"Amount exceeds the transaction limit of {Bank.Currency} {Bank.TransactionLimit}");
            }
        }

        public void WithdrawAmount(decimal amount)
        {
            if (amount <= 0m) throw new InvalidOperationException($"Amount to deposit can not be less than {Bank.Currency} 0.00");
            if (amount <= Bank.TransactionLimit)
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
                throw new InvalidOperationException($"Amount exceeds the transaction limit of {Bank.Currency} {Bank.TransactionLimit}");
            }
        }
    }
}
