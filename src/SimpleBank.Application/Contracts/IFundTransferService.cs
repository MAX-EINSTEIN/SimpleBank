using SimpleBank.Application.DTOs;
using SimpleBank.Domain.Models;

namespace SimpleBank.Application.Contracts
{
    public interface IFundTransferService
    {
        public Task<FundTransfer?> GetByUTRNumber(string UTRNumber);
        public Task<FundTransfer?> Process(CreateFundTransferDTO dto);
    }
}
