using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tea_bank.Data;
using tea_bank.DTOs;
using AutoMapper;
using Tea_Bank_Backend.Services;
using Tea_Bank_Backend.DTOs;

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
        public async Task<List<BankAccount>> AddAccount(BankAccDTO bankAcc)
        {
            var newBankAccout = new BankAccount
            {
                DateOfOPening = DateTime.Now,
                Balance = bankAcc.Balance,
                Currency = bankAcc.Currency,
                Type = bankAcc.Type,
                UserId = bankAcc.UserId
            };
            _context.BankAccounts.Add(newBankAccout);
            await _context.SaveChangesAsync();

            return await _context.BankAccounts.ToListAsync();
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

        async Task<List<BankAccount>> IBankAccService.UpdateAccount(int id, BankAccDTO bankAcc)
        {
            var bankAccToUpdate = _context.BankAccounts.FindAsync(id);
            if (bankAccToUpdate == null)
            {
                return null;
            }

            bankAccToUpdate.Result.Balance = bankAcc.Balance;
            bankAccToUpdate.Result.Currency = bankAcc.Currency;
            bankAccToUpdate.Result.Type = bankAcc.Type;
            bankAccToUpdate.Result.UserId = bankAcc.UserId;

            _context.BankAccounts.Update(bankAccToUpdate.Result);
            _context.SaveChanges();

            return await _context.BankAccounts.ToListAsync();
        }
    }
}