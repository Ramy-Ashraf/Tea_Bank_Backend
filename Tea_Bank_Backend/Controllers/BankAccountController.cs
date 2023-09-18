using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Models;
using tea_bank.Services;
using Tea_Bank_Backend.DTOs;
using Tea_Bank_Backend.Services;
namespace Tea_Bank_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccService _bankAccService;
        public BankAccountController(IBankAccService bankAccService)
        {
            _bankAccService = bankAccService;
        }
        [HttpGet, Authorize]
        public async Task<ActionResult<List<BankAccount>>> GetAllAccounts()
        {
            return await _bankAccService.GetAllAccounts();
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<BankAccount>> GetAccountById(int id)
        {
            var result = await _bankAccService.GetAccountById(id);
            if (result is null)
            {
                return NotFound("Bank Account not Found.");
            }

            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<BankAccount>>> AddAccount(BankAccDTO bankAcc)
        {
            var result = await _bankAccService.AddAccount(bankAcc);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<BankAccount>>> DeleteAccount(int id)
        {
            var result = await _bankAccService.DeleteAccount(id);
            if (result is null)
            {
                return NotFound("Bank Account not Found.");
            }

            return Ok(result);
        }
        // update account
        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<List<BankAccount>>> UpdateAccount(int id, BankAccDTO bankAcc)
        {
            var result = await _bankAccService.UpdateAccount(id, bankAcc);
            if (result is null)
            {
                return NotFound("Bank Account not Found.");
            }

            return Ok(result);
        }
    }
}