using Microsoft.AspNetCore.Mvc;
using SimpleBank.Domain.BankAggregate;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Shared;
using SimpleBank.API.DTOs;

namespace SimpleBank.API.Controllers
{
    [ApiController]
    [Route("api/banks")]
    public class BankController : ControllerBase
    {
        private readonly IRepository<Bank> _bankRepository;
        private readonly ILogger<BankController> _logger;

        public BankController(IRepository<Bank> bankRepository, ILogger<BankController> logger)
        {
            _bankRepository = bankRepository;
            _logger = logger;
        }

        [Route("list")]
        [HttpGet]
        public async Task<IEnumerable<Bank>> Get()
        {
            return await _bankRepository.List();
        }

        [HttpGet("detail/{bankId}")]
        public async Task<ActionResult<Bank>> Get(long bankId)
        {
            var bank = await _bankRepository.GetById(bankId);
            if (bank is null) { return NotFound();  }

            return Ok(bank);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<Bank>> Post(CreateBankDTO dto)
        {
            Bank bank = new (
                dto.Name,
                new Address(
                    dto.Street,
                    dto.City,
                    dto.Region,
                    dto.Country,
                    dto.ZipCode
                ),
                dto.TransactionLimit,
                dto.Currency
            );
            await _bankRepository.Add(bank);

            return CreatedAtAction(
                        nameof(Get), 
                        new { bankId = bank.Id}, 
                        bank
                    );
        }
    }

}

