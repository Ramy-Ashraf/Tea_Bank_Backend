using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tea_bank.Data;
using tea_bank.DTOs;
using AutoMapper;
using Tea_Bank_Backend.Services;

namespace tea_bank.Services
{
    public class BankAccService : IBankAccService
    {
        private readonly ApplicationDbContext _context;
        public BankAccService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> AddAccount(BankAccDTO bankAcc)
        {
            var newBankAcc = new BankAccount
            {
                Balance = bankAcc.Balance,
                Currency = bankAcc.Currency,
                Type = bankAcc.Type,
                User = User.
                
            };

            
            _context.BankAccounts.Add(newBankAcc);
            await _context.SaveChangesAsync();

            return await _context.Guests.ToListAsync();
        }
        public async Task<List<Guest>> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
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

