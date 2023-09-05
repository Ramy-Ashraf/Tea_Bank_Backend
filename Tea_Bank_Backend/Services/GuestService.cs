using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tea_bank.Data;
using tea_bank.DTOs;
using AutoMapper;

namespace tea_bank.Services
{
    public class GuestService: IGuestService
    {
        private readonly ApplicationDbContext _context;
        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> AddGuest(GuestDTO guest)
        {
            var newGuest = new Guest
            {
                Name = guest.Name,
                Email = guest.Email,
                Message = guest.Message
            };
            _context.Guests.Add(newGuest);
            await _context.SaveChangesAsync();

            return await _context.Guests.ToListAsync();
        }
        public async Task<List<Guest>> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if(guest == null)
            {
                return null;
            }
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return await _context.Guests.ToListAsync();
        }

        public async Task<List<Guest>> GetAllGuests()
        {
            var guests = await _context.Guests.ToListAsync();
            return guests;
        }

        public async Task<Guest> GetGuestById(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return null;
            }

            return guest;
        }
    }
}
