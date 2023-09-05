using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Services;
using Tea_Bank_Backend.Services;
using BankAccount = tea_bank.Models.BankAccount;

namespace Tea_Bank_Backend.Controllers
{
    public class BankAccController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BankAccountController : ControllerBase
        {
            private readonly IBankAccService _bankAccService;
            public BankAccController(IBankAccService bankAccService)
            {
                _bankAccService = bankAccService;
            }
            [HttpGet]
            public async Task<ActionResult<List<BankAccount>>> GetAllAccounts()
            {
                return await _bankAccService.GetAllAccounts();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Guest>> GetGuestById(int id)
            {
                var result = await _guestService.GetGuestById(id);
                if (result is null)
                {
                    return NotFound("Guest not Found.");
                }

                return Ok(result);
            }
            [HttpPost]
            public async Task<ActionResult<List<Guest>>> AddGuest(GuestDTO guest)
            {
                var result = await _guestService.AddGuest(guest);

                return Ok(result);
            }
            [HttpDelete("{id}")]
            public async Task<ActionResult<List<Guest>>> DeleteGuest(int id)
            {
                var result = await _guestService.DeleteGuest(id);
                if (result is null)
                {
                    return NotFound("Guest not Found.");
                }

                return Ok(result);
            }
        }
    }
}
