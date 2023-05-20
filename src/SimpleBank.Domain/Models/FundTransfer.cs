using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Utils;
using System.Transactions;

namespace SimpleBank.Domain.Models
{
    public class FundTransfer: Entity, IAggregateRoot
    {
        public string SourceAccountNumber { get; }
        public string SourceAccountBranchIFSC { get; }

        public string DestinationAccountNumber { get; }
        public string DestinationAccountBranchIFSC { get; }

        private decimal _amount = decimal.Zero;
        public decimal Amount { get => Math.Round(_amount, 2); }

        public string BankReferenceNo { get; }
        public string UTRNumber => BankReferenceNo;

        public string? PaymentMode { get; }
        public string Remarks { get; }

        public DateTime TransferDate { get;  }

        public FundTransfer(
                            string sourceAccountNumber,
                            string sourceAccountBranchIFSC,
                            string destinationAccountNumber,
                            string destinationAccountBranchIFSC,
                            decimal amount,
                            string? paymentMode,
                            string remarks)
        {
            SourceAccountNumber = sourceAccountNumber;
            SourceAccountBranchIFSC = sourceAccountBranchIFSC;
            DestinationAccountNumber = destinationAccountNumber;
            DestinationAccountBranchIFSC = destinationAccountBranchIFSC;
            _amount = amount;
            BankReferenceNo = AlphaNumericGenerator.GetRandomNumbers(8);
            PaymentMode = paymentMode;
            Remarks = remarks;
        }

        public void TransferAmount(Bank sourceBank, Bank destinationBank)
        {
            var sourceAccount = sourceBank.Accounts.Where(a => a.AccountNumber == SourceAccountNumber).SingleOrDefault();
            if (sourceAccount is null)
                throw new InvalidOperationException("The Provided Source Account Number is incorrect.");

            var destinationAccount = destinationBank.Accounts.Where(a => a.AccountNumber == DestinationAccountNumber).SingleOrDefault();
            if (destinationAccount is null)
                throw new InvalidOperationException("The Provided Source Account Number is incorrect.");

            using var txnScope = new TransactionScope(TransactionScopeOption.RequiresNew);
            try
            {
                var withdrawDescription = (
                    $"To {PaymentMode} UTR {BankReferenceNo} - "
                    + $"Sent To {destinationAccount.AccountHolder.Name} - "
                    + $"Transfer to A/C No {DestinationAccountNumber}"
                    ).ToUpperInvariant();

                sourceAccount.WithdrawAmount(Amount, withdrawDescription);

                var depositDescription = (
                    $"By {PaymentMode} UTR {BankReferenceNo} - "
                    + $"Sent From {sourceAccount.AccountHolder.Name} - "
                    + $"Transfer From A/C No {SourceAccountNumber}"
                    ).ToUpperInvariant();

                destinationAccount.DepositAmount(Amount, depositDescription);

                txnScope.Complete();
            }
            catch (Exception ex)
            {
                txnScope.Dispose();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
