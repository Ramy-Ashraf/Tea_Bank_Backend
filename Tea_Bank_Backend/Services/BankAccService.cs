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
        //private readonly UserService _userService;

        public BankAccService(ApplicationDbContext context)
        {
            _context = context;
            //_userService = userService;
        }


        public async Task<List<BankAccount>> DeleteAccount(int id)
        {
            var bankAcc = await _context.BankAccounts.FindAsync(id);
            if (bankAcc == null)
            {
                return null;
            }
            _context.BankAccounts.Remove(bankAcc);
            await _context.SaveChangesAsync();

            return await _context.BankAccounts.ToListAsync();
        }


        public async Task<List<BankAccount>> GetAllAccounts()
        {
            var bankAcc = await _context.BankAccounts.ToListAsync();
            return bankAcc;
        }

        public async Task<BankAccount> GetAccountById(int id)
        {
            var bankAcc = await _context.BankAccounts.FindAsync(id);
            if (bankAcc == null)
            {
                return null;
            }

            return bankAcc;
        }

        //public async Task<List<BankAccount>> AddAccount(int id, BankAccDTO bankAcc)
        //{
        //    Task<User> user = _userService.GetUserById(id);
        //    var newBankAcc = new BankAccount
        //    {
        //        Balance = bankAcc.Balance,
        //        Currency = bankAcc.Currency,
        //        Type = bankAcc.Type,
        //        User = await user
        //    };
        //    _context.BankAccounts.Add(newBankAcc);
        //    /*bankAcc.User.set(user);*/ // Set the user for the bank account
        //    await _context.SaveChangesAsync();

        //    return await _context.BankAccounts.ToListAsync();
        //}
    }
}

