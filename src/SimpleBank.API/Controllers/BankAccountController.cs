using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;
using SimpleBank.Domain.Services;

namespace SimpleBank.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountManagementDomainService _accountService;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILogger<BankController> _logger;

        public BankAccountController(IBankAccountManagementDomainService accountService, IBankAccountRepository bankAccountRepository, ILogger<BankController> logger)
        {
            _accountService = accountService;
            _bankAccountRepository = bankAccountRepository;
            _logger = logger;
        }

        [Route("accounts/list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> Index([FromQuery]string bankCode)
        {
            var accounts = await _bankAccountRepository.List(a => a.BranchIFSC.Substring(0, 4) == bankCode);
            if (accounts is null) return NotFound();
            return Ok(accounts);
        }

        [Route("account/details")]
        [HttpGet]
        public async Task<ActionResult<BankAccount>> Get([FromQuery]long accountId)
        {
            var account = await _bankAccountRepository.GetById(accountId);
       
            if (account is null) return NotFound();

            return Ok(account);
        }

        [Route("account/statement")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionRecord>>> GetAccountStatetement([FromQuery]string AccountNumber,[FromQuery] string IFSC)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(AccountNumber, IFSC);

            if (account is null) return NotFound();

            return Ok(account.TransactionRecords);
        }

        [Route("accounts/create")]
        [HttpPost]
        public async Task<ActionResult<BankAccount>> Post(CreateAccountDTO dto)
        {
            var address = new Address(dto.Street, dto.City, dto.Region, dto.Country, dto.ZipCode);
            var accountHolder = new Customer(dto.Name, dto.Gender, dto.Email, dto.PhoneNumber, address);
            var account = await _accountService.CreateAccount(accountHolder,
                                                              dto.TransactionLimit,
                                                              dto.Currency,
                                                              dto.BankCode,
                                                              dto.BranchCode);

            return CreatedAtAction(
                        nameof(Get),
                        new
                        {
                            accountId = account.Id
                        },
                        account
                    );
        }

        [HttpPut("account/{accountNumber}/{IFSC}/deposit/{amount}")]
        public async Task<IActionResult> Deposit(string accountNumber, string IFSC, decimal amount)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(accountNumber, IFSC);

            if (account is null) return NotFound();

            account.DepositAmount(amount);

            await _bankAccountRepository.Update(account);

            return AcceptedAtAction(nameof(Get),
                new
                {
                    accountId = account.Id
                },
                account
                );
        }

        [HttpPut("account/{accountNumber}/{IFSC}/withdraw/{amount}")]
        public async Task<IActionResult> Withdraw(string accountNumber, string IFSC, decimal amount)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(accountNumber, IFSC);

            if (account is null) return NotFound();

            account.DepositAmount(amount);

            await _bankAccountRepository.Update(account);

            return AcceptedAtAction(nameof(Get),
                new
                {
                    accountId = account.Id
                },
                account
                );
        }

        [HttpDelete("accounts/{accountId}/delete/")]
        public async Task<IActionResult> Delete(long accountId)
        { 
            var account = await _bankAccountRepository.GetById(accountId);

            if (account is null) return NotFound();

            await _accountService.DeleteAccount(account.AccountNumber, account.BranchIFSC);
            
            return NoContent();
        }
    }
}