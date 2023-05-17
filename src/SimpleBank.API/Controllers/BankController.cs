using Microsoft.AspNetCore.Mvc;
using SimpleBank.Domain.Contracts;
using SimpleBank.API.DTOs;
using SimpleBank.Domain.Models;

namespace SimpleBank.API.Controllers
{
    [ApiController]
    [Route("api/banks")]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly ILogger<BankController> _logger;

        public BankController(IBankRepository bankRepository, ILogger<BankController> logger)
        {
            _bankRepository = bankRepository;
            _logger = logger;
        }

        [Route("list")]
        [HttpGet]
        public async Task<IEnumerable<Bank>> Index()
        {
            return await _bankRepository.List();
        }

        [HttpGet("{bankId}/details")]
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
                )
            );
            await _bankRepository.Add(bank);

            return CreatedAtAction(
                        nameof(Get), 
                        new { bankId = bank.Id}, 
                        bank
                    );
        }

        [HttpDelete("{bankId}/delete")]
        public async Task<IActionResult> Delete(long bankId)
        {
            var bank = await _bankRepository.GetById(bankId);

            if (bank is null) { return NotFound(); }

            await _bankRepository.Delete(bank);

            return NoContent();
        }
    }

}

