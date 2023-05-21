using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.DTOs;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;

namespace SimpleBank.API.Controllers
{
    [ApiController]
    [Route("api/bank/")]
    public class BankBranchesController: ControllerBase
    {
        private readonly IBankBranchManagementService _bankBranchManagementService;
        private readonly IBankBranchRepository _bankBranchRepository;

        public BankBranchesController(IBankBranchManagementService bankBranchManagementService,
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

        [HttpPost]
        public async Task<IActionResult> Post(CreateBankBranchDTO dto)
        {
            var address = new Address(dto.Street, dto.City, dto.Region, dto.Country, dto.ZipCode);
            var bankBranch = await _bankBranchManagementService
                .AddBankBranch(dto.BankId, dto.Name, address);

            if (bankBranch is null)
                return BadRequest();

            return CreatedAtAction(nameof(Get),
                                    new { bankBranch.BranchCode },
                                    bankBranch
                                    );
        }

        [HttpDelete("{bankId}/branch/{branchId}/delete")]
        public async Task<IActionResult> Delete(long bankId, long branchId)
        {
            var deleted = await _bankBranchManagementService.RemoveBankBranch(bankId, branchId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
