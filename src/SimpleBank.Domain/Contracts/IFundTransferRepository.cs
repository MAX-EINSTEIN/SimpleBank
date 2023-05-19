using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Contracts
{
    public interface IFundTransferRepository: IRepository<FundTransfer>
    {
        Task<FundTransfer?> GetByUTRNumber(string UTRNumber);
    }
}
