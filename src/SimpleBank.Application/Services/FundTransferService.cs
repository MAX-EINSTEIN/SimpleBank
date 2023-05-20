using SimpleBank.Application.Contracts;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;

namespace SimpleBank.Application.Services
{
    public class FundTransferService : IFundTransferService
    {
        private readonly IFundTransferRepository _fundTransferRepository;
        private readonly IBankRepository _bankRepository;

        public FundTransferService(IFundTransferRepository fundTransferRepository, IBankRepository bankRepository)
        {
            _fundTransferRepository = fundTransferRepository;
            _bankRepository = bankRepository;
        }

        public async Task<FundTransfer?> GetByUTRNumber(string UTRNumber)
        {
            return await _fundTransferRepository.GetByUTRNumber(UTRNumber);
        }

        public async Task<FundTransfer?> Process(CreateFundTransferDTO dto)
        {
            var fundTransfer = new FundTransfer(
                                    dto.SourceAccountNumber,
                                    dto.SourceAccountBranchIFSC,
                                    dto.DestinationAccountNumber,
                                    dto.DestinationAccontBranchIFSC,
                                    dto.Amount,
                                    dto.PaymentMode,
                                    dto.Remarks
                                );

            if (fundTransfer is null)
                return null;

            fundTransfer = await _fundTransferRepository.Add(fundTransfer);

            var sourceBank = await _bankRepository.GetByIFSC(dto.SourceAccountBranchIFSC);
            var destinationBank = await _bankRepository.GetByIFSC(dto.DestinationAccontBranchIFSC);

            if (sourceBank is null || destinationBank is null)
                return null;

            try
            {
                fundTransfer.TransferAmount(sourceBank, destinationBank);

                await _bankRepository.Update(sourceBank);
                await _bankRepository.Update(destinationBank);
            }
            catch
            {
                return null;
            }

            await _fundTransferRepository.Update(fundTransfer);

            return fundTransfer;
        }
    }
}
