using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;

namespace SimpleBank.API.Controllers
{
    [ApiController]
    [Route("api/bank")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly ILogger<BankController> _logger;

        public BankAccountController(IBankRepository bankRepository, ILogger<BankController> logger)
        {
            _bankRepository = bankRepository;
            _logger = logger;
        }

        [Route("{bankId}/accounts/list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> Index(long bankId)
        {
            var bank = await _bankRepository.GetById(bankId);
            if (bank is null) return NotFound();
            return Ok(bank.Accounts);
        }

        [Route("{bankId}/accounts/{accountId}/details")]
        [HttpGet]
        public async Task<ActionResult<BankAccount>> Get(long bankId, long accountId)
        {
            var bank = await _bankRepository.GetById(bankId);
            
            if (bank is null) return NotFound();
            
            var account = bank.Accounts
                            .Where(a => a.Id == accountId)
                            .FirstOrDefault();

            if (account is null) return NotFound();

            return Ok(account);
        }

        [Route("{branchIFSC}/accounts/{accountId}/statement")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionRecord>>> GetAccountStatetement(string branchIFSC, long accountId)
        {
            var bank = await _bankRepository.GetByIFSC(branchIFSC, true);

            if (bank is null) return NotFound();

            var account = bank.Accounts
                            .Where(a => a.Id == accountId)
                            .FirstOrDefault();

            if (account is null) return NotFound();

            return Ok(account.TransactionRecords);
        }

        [Route("{bankId}/accounts/create")]
        [HttpPost]
        public async Task<ActionResult<BankAccount>> Post(long bankId, CreateAccountDTO dto)
        {
            var bank = await _bankRepository.GetById(bankId);

            if (bank is null) return NotFound();

            var address = new Address(dto.Street, dto.City, dto.Region, dto.Country, dto.ZipCode);
            var accountHolder = new Customer(dto.Name, dto.Gender, dto.Email, dto.PhoneNumber, address);

            var account = bank.CreateAccount(accountHolder, dto.TransactionLimit, dto.Currency);

            await _bankRepository.Update(bank);

            return CreatedAtAction(
                        nameof(Get),
                        new
                        {
                            bankId = bank.Id,
                            accountId = account.Id
                        },
                        account
                    );
        }

        [HttpPut("{bankId}/accounts/{accountId}/deposit/{amount}")]
        public async Task<IActionResult> Deposit(long bankId, long accountId, decimal amount)
        {
            var bank = await _bankRepository.GetById(bankId);
            if (bank is null)  return NotFound(); 

            var account = bank.Accounts
                            .Where(a => a.Id == accountId)
                            .FirstOrDefault();

            if (account is null) return NotFound();

            account.DepositAmount(amount);

            await _bankRepository.Update(bank);

            return AcceptedAtAction(nameof(Get),
                new
                {
                    bankId = bank.Id,
                    accountId = account.Id
                },
                account
                );
        }

        [HttpPut("{bankId}/accounts/{accountId}/withdraw/{amount}")]
        public async Task<IActionResult> Withdraw(long bankId, long accountId, decimal amount)
        {
            var bank = await _bankRepository.GetById(bankId);
            if (bank is null) return NotFound();

            var account = bank.Accounts
                            .Where(a => a.Id == accountId)
                            .FirstOrDefault();

            if (account is null) return NotFound();

            account.WithdrawAmount(amount);

            await _bankRepository.Update(bank);

            return AcceptedAtAction(nameof(Get),
                new
                {
                    bankId = bank.Id,
                    accountId = account.Id
                },
                account
                );
        }

        [HttpDelete("{bankId}/accounts/{accountId}/delete/")]
        public async Task<IActionResult> Delete(long bankId, long accountId)
        {
            var bank = await _bankRepository.GetById(bankId);
            if (bank is null) { return NotFound(); }

            if (!bank.DeleteAccount(accountId)) return NotFound();

            await _bankRepository.Update(bank);

            return NoContent();
        }
    }
}