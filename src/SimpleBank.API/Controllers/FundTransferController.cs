using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.Contracts;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.Models;

namespace SimpleBank.API.Controllers
{
    [Route("/api/transfer/")]
    [ApiController]
    public class FundTransferController: ControllerBase
    {
        private readonly IFundTransferService _fundTransferService;
        private readonly ILogger<FundTransfer> _logger;

        public FundTransferController(IFundTransferService fundTransferService, ILogger<FundTransfer> logger)
        {
            _fundTransferService = fundTransferService;
            _logger = logger;
        }

        [HttpGet("{UTRNumber}/details")]
        public async Task<ActionResult<FundTransfer?>> Get(string UTRNumber)
        {
            var fundTransfer = await _fundTransferService.GetByUTRNumber(UTRNumber);

            if (fundTransfer is null) return NotFound();

            return Ok(fundTransfer); 
        }

        [HttpPost]
        public async Task<ActionResult<FundTransfer>> Create(CreateFundTransferDTO dto)
        {

            var fundTransfer = await _fundTransferService.Process(dto);

            if (fundTransfer is null) return NotFound();

            return CreatedAtAction(
                    nameof(Get),
                    new { fundTransfer.UTRNumber },
                    fundTransfer
                );
        }

    }
}
