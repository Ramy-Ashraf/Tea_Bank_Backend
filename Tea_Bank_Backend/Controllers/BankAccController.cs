using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Services;
using Tea_Bank_Backend.Services;
using tea_bank.Models;

namespace Tea_Bank_Backend.Controllers
{
    public class BankAccController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BankAccountController : ControllerBase
        {
            private readonly IBankAccService _bankAccService;
            public BankAccountController (IBankAccService bankAccService)
            {
                _bankAccService = bankAccService;
            }
            [HttpGet]
            public async Task<ActionResult<List<BankAccount>>> GetAllAccounts()
            {
                return await _bankAccService.GetAllAccounts();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<BankAccount>> GetAccountById(int id)
            {
                var result = await _bankAccService.GetAccountById(id);
                if (result is null)
                {
                    return NotFound("Bank Account not Found.");
                }

                return Ok(result);
            }
            [HttpPost]
            //public async Task<ActionResult<List<BankAccount>>> AddAccount(BankAccDTO bankAcc)
            //{
            //    var result = await _bankAccService.AddAccount(bankAcc);

            //    return Ok(result);
            //}
            [HttpDelete("{id}")]
            public async Task<ActionResult<List<BankAccount>>> DeleteAccount(int id)
            {
                var result = await _bankAccService.DeleteAccount(id);
                if (result is null)
                {
                    return NotFound("Bank Account not Found.");
                }

                return Ok(result);
            }
        }
    }
}
