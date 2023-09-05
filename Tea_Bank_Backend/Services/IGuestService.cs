using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Models;

namespace tea_bank.Services
{
    public interface IGuestService
    {
        Task<List<Guest>> AddGuest(GuestDTO guest);
        Task<List<Guest>> DeleteGuest(int id);
        Task<Guest> GetGuestById(int id);
        Task<List<Guest>> GetAllGuests();
    }
}
