using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Common;

namespace SimpleBank.Domain.BankAccountAggregate
{
    public class TransactionRecord : Entity
    {
        public string ReferenceId { get; }
        public string Description { get; }
        public decimal DebitedAmount { get; }
        public decimal CreditedAmount { get; }
        public decimal UpdatedBalance { get; }
        public string TransactionType { get; }
        public DateTime TransactionDate { get; }

        public TransactionRecord() { }

        public TransactionRecord(string description, decimal debitedAmount, decimal creditAmount, decimal updatedBalance)
        {
            ReferenceId = AlphaNumericGenerator.GetRandomAlphaNumeric(4, 8);
            Description = description;
            DebitedAmount = debitedAmount;
            CreditedAmount = creditAmount;
            UpdatedBalance = updatedBalance;
            TransactionType = (DebitedAmount == 0m ? "Credit" : "Debit").ToUpperInvariant();
        }

    }
}
