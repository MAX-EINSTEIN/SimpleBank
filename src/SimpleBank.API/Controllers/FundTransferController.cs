using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.DTOs;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;

namespace SimpleBank.API.Controllers
{
    [Route("/api/transfer/")]
    [ApiController]
    public class FundTransferController: ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly IFundTransferRepository _fundTransferRepository;
        private readonly ILogger<FundTransfer> _logger;

        public FundTransferController(IBankRepository bankRepository, IFundTransferRepository fundTransferRepository, ILogger<FundTransfer> logger)
        {
            _bankRepository = bankRepository;
            _fundTransferRepository = fundTransferRepository;
            _logger = logger;
        }

        [HttpGet("{UTRNumber}/details")]
        public async Task<ActionResult<FundTransfer?>> Get(string UTRNumber)
        {
            var fundTransfer = await _fundTransferRepository.GetByUTRNumber(UTRNumber);

            if (fundTransfer is null) return NotFound();

            return Ok(fundTransfer); 
        }

        [HttpPost]
        public async Task<ActionResult<FundTransfer>> Create(CreateFundTransferDTO dto)
        {
            var fundTransfer = new FundTransfer(
                                    dto.SourceAccountNumber,
                                    dto.SourceAccountBranchIFSC,
                                    dto.DestinationAccountNumber,
                                    dto.DestinationAccontBranchIFSC,
                                    dto.PaymentMode,
                                    dto.Remarks
                                );

            if (fundTransfer is null) return BadRequest();

            fundTransfer = await _fundTransferRepository.Add(fundTransfer);
            var sourceBank = await _bankRepository.GetByIFSC(dto.SourceAccountBranchIFSC);
            var destinationBank = await _bankRepository.GetByIFSC(dto.DestinationAccontBranchIFSC);

            if (sourceBank is null || destinationBank is null)
                return BadRequest();

            try
            {
                fundTransfer.TransferAmount(dto.Amount, sourceBank, destinationBank);

                await _bankRepository.Update(sourceBank);
                await _bankRepository.Update(destinationBank);
            } catch (Exception ex)
            {
                return BadRequest();
            }

            await _fundTransferRepository.Update(fundTransfer);

            return CreatedAtAction(
                    nameof(Get),
                    new { UTRNumber = fundTransfer.UTRNumber },
                    fundTransfer
                );
        }

    }
}


