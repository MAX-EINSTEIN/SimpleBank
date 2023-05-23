using SimpleBank.Application.Contracts;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.BankAccountAggregate;
using SimpleBank.Domain.FundTransferAggregate;

namespace SimpleBank.Application.Services
{
    public class FundTransferService : IFundTransferService
    {
        private readonly IFundTransferRepository _fundTransferRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public FundTransferService(IFundTransferRepository fundTransferRepository, IBankAccountRepository bankAccountRepository)
        {
            _fundTransferRepository = fundTransferRepository;
            _bankAccountRepository = bankAccountRepository;
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

            try
            {
                var sourceAccount = await _bankAccountRepository.GetByAccountNumberAndIFSC(dto.SourceAccountNumber, dto.SourceAccountBranchIFSC);
                var destinationAccount = await _bankAccountRepository.GetByAccountNumberAndIFSC(dto.DestinationAccountNumber, dto.DestinationAccontBranchIFSC);

                if (sourceAccount is null || destinationAccount is null)
                    throw new InvalidOperationException("Source Or Destination Account does not exists");

                fundTransfer.TransferAmount(sourceAccount, destinationAccount);

                await _bankAccountRepository.Update(sourceAccount);
                await _bankAccountRepository.Update(destinationAccount);
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
