using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost("{id}")]
        //public async Task<ActionResult<List<BankAccount>>> AddAccount(int id, BankAccDTO bankAcc)
        //{
        //    return await _bankAccService.AddAccount(id, bankAcc);
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
