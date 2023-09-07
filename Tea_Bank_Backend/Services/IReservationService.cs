using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Models;
using Tea_Bank_Backend.DTOs;

namespace Tea_Bank_Backend.Services
{
   
    public interface IReservationService
    {
        Task<List<Reservation>> ADDReservation(ReservationDTO reservation);
        Task<Reservation> GetReservationByID(int id);
        Task<List<Reservation>> GetALLReservations();
        Task<List<Reservation>> DeleteReservation(int id);

    }
}
