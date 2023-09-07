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
    [ApiController,Authorize]
    public class ReservationController : ControllerBase
    {

        private readonly IReservationService _reservation;


        public ReservationController(IReservationService reservation)
        {
            _reservation = reservation;
        }

        [HttpGet("AllReservations"),Authorize]
        public async Task<ActionResult<List<Reservation>>> GetALLReservations()
        {
            return await _reservation.GetALLReservations();
        }
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            var result = await _reservation.GetReservationByID(id);
            if (result is null)
            {
                return NotFound("Reservation not Found.");
            }

            return Ok(result);
        }
        [HttpPost,Authorize]
        public async Task<ActionResult<List<Reservation>>> ADDReservation(ReservationDTO reservation)
        {
            var result = await _reservation.ADDReservation(reservation);

            return Ok(result);
        }
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult<List<Reservation>>> DeleteReservation(int id)
        {
            var result = await _reservation.DeleteReservation(id);
            if (result is null)
            {
                return NotFound("Reservation not Found.");
            }

            return Ok(result);
        }
    }
}


