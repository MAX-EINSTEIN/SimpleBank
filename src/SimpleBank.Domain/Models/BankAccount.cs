using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.Models
{
    public class BankAccount : Entity, IAggregateRoot
    {
        public string AccountNumber { get; }

        public string BranchIFSC { get; }

        public decimal TransactionLimit { get; }

        public string Currency { get; }

        private decimal _balance = decimal.Zero;
        public decimal Balance => Math.Round(_balance, 2);

        private readonly List<TransactionRecord> _transactionRecords = new();
        public IList<TransactionRecord> TransactionRecords => _transactionRecords.AsReadOnly(); 

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

        private void AddTransactionRecord(string description, decimal debitedAmount, decimal creditedAmount, decimal updatedBalance)
        {
            TransactionRecord trasaction = new(description, debitedAmount, creditedAmount, Balance);
            _transactionRecords.Add(trasaction);
        }

        public void DepositAmount(decimal amount, string? description = null)
        {
            if (amount <= 0m) throw new InvalidOperationException($"Amount to deposit can not be less than {Currency} 0.00");
            if (amount <= TransactionLimit)
            {
                UpdateBalance(_balance + amount);

                if (string.IsNullOrEmpty(description))
                    description = $"A/C {AccountNumber} credited with {amount}".ToUpperInvariant();

                AddTransactionRecord(description, 0m, amount, Balance);
            }
            else
            {
                throw new InvalidOperationException($"Amount exceeds the transaction limit of {Currency} {TransactionLimit}");
            }
        }

        public void WithdrawAmount(decimal amount, string? description = null)
        {
            if (amount <= 0m) throw new InvalidOperationException($"Amount to deposit can not be less than {Currency} 0.00");
            if (amount <= TransactionLimit)
            {
                try
                {
                    UpdateBalance(Balance - amount);

                    if (string.IsNullOrEmpty(description))
                        description = $"A/C {AccountNumber} debited with {amount}".ToUpperInvariant();

                    AddTransactionRecord(description, amount, 0m, Balance);
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
