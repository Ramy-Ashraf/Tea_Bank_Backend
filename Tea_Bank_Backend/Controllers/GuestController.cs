using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using tea_bank.DTOs;
using tea_bank.Services;

namespace tea_bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Guest>>> GetAllGuests()
        {
            return await _guestService.GetAllGuests();
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
