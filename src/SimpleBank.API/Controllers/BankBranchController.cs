using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.BankBranchAggregate;
using SimpleBank.Domain.Common;

namespace SimpleBank.API.Controllers
{
    [ApiController]
    [Route("api/bank/")]
    public class BankBranchController: ControllerBase
    {
        private readonly IBankBranchManagementService _bankBranchManagementService;
        private readonly IBankBranchRepository _bankBranchRepository;

        public BankBranchController(IBankBranchManagementService bankBranchManagementService,
                                      IBankBranchRepository bankBranchRepository)
        {
            _bankBranchManagementService = bankBranchManagementService;
            _bankBranchRepository = bankBranchRepository;
        }

        [HttpGet("branch/{branchCode}/detail")]
        public async Task<ActionResult<BankBranch?>> Get(string branchCode)
        {
            return Ok(await _bankBranchRepository.GetByBranchCode(branchCode));
        }

        [HttpPost("{bankCode}/branches/create")]
        public async Task<IActionResult> Post(string bankCode, CreateBankBranchDTO dto)
        {
            var address = new Address(dto.Street, dto.City, dto.Region, dto.Country, dto.ZipCode);
            var bankBranch = await _bankBranchManagementService
                .AddBankBranch(bankCode, dto.Name, address);

            if (bankBranch is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get),
                                    new { bankBranch.BranchCode },
                                    bankBranch
                                    );
        }

        [HttpDelete("{bankCode}/branch/{branchCode}/delete")]
        public async Task<IActionResult> Delete(string bankCode, string branchCode)
        {
            var deleted = await _bankBranchManagementService.RemoveBankBranch(bankCode, branchCode);
            return deleted ? NoContent() : NotFound();
        }
    }
}
