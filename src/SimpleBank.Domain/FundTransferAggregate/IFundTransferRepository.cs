using SimpleBank.Domain.Contracts;

namespace SimpleBank.Domain.FundTransferAggregate
{
    public interface IFundTransferRepository : IRepository<FundTransfer>
    {
        Task<FundTransfer?> GetByUTRNumber(string UTRNumber);
    }
}
