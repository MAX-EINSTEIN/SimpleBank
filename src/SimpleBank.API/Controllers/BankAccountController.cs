using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.BankAccountAggregate;
using SimpleBank.Domain.Common;

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

        [HttpGet("bank/{bankCode}/accounts")]
        public async Task<ActionResult<IEnumerable<BankAccount>>> Index(string bankCode)
        {
            var accounts = await _bankAccountRepository.List(a => a.BranchIFSC.Substring(0, 4) == bankCode);
            if (accounts is null) return NotFound();
            return Ok(accounts);
        }

        [HttpGet("bank/account/details")]
        public async Task<ActionResult<BankAccount>> Get([FromQuery]string accountNumber, [FromQuery] string IFSC)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(accountNumber, IFSC);
       
            if (account is null) return NotFound();

            return Ok(account);
        }

        [HttpGet("bank/account/statement")]
        public async Task<ActionResult<IEnumerable<TransactionRecord>>> GetAccountStatetement([FromQuery]string AccountNumber,[FromQuery] string IFSC)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(AccountNumber, IFSC);

            if (account is null) return NotFound();

            return Ok(account.TransactionRecords);
        }

        [HttpPost("bank/{bankCode}/branch/{branchCode}/accounts/create")]
        public async Task<ActionResult<BankAccount>> Post(string bankCode, string branchCode, [FromBody] CreateAccountDTO dto)
        {
            var address = new Address(dto.Street, dto.City, dto.Region, dto.Country, dto.ZipCode);
            var accountHolder = new Customer(dto.Name, dto.Gender, dto.Email, dto.PhoneNumber, address);
            var account = await _accountService.CreateAccount(accountHolder,
                                                              dto.TransactionLimit,
                                                              dto.Currency,
                                                              bankCode,
                                                              branchCode);

            return CreatedAtAction(
                        nameof(Get),
                        new
                        {
                            accountNumber = account.AccountNumber,
                            IFSC = account.BranchIFSC
                        },
                        account
                    );
        }

        [HttpPut("bank/account/deposit/")]
        public async Task<IActionResult> Deposit(string accountNumber, string IFSC, decimal amount)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(accountNumber, IFSC);

            if (account is null) return NotFound();

            account.DepositAmount(amount);

            await _bankAccountRepository.Update(account);

            return AcceptedAtAction(nameof(Get),
                new
                {
                    accountNumber = account.AccountNumber,
                    IFSC = account.BranchIFSC
                },
                account
                );
        }

        [HttpPut("bank/account/withdraw")]
        public async Task<IActionResult> Withdraw(string accountNumber, string IFSC, decimal amount)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAndIFSC(accountNumber, IFSC);

            if (account is null) return NotFound();

            account.DepositAmount(amount);

            await _bankAccountRepository.Update(account);

            return AcceptedAtAction(nameof(Get),
                new
                {
                    accountNumber = account.AccountNumber,
                    IFSC = account.BranchIFSC
                },
                account
                );
        }

        [HttpDelete("bank/account/delete/")]
        public async Task<IActionResult> Delete(string accountNumber, string IFSC)
        { 
            return await _accountService.DeleteAccount(accountNumber, IFSC) 
                ? NoContent()
                : NotFound();
        }
    }
}