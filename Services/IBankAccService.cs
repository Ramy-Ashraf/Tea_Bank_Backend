using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Models;
using Tea_Bank_Backend.DTOs;

namespace Tea_Bank_Backend.Services
{
    public interface IBankAccService
    {
        //Task<List<BankAccount>> AddAccount(int id, BankAccDTO bankAcc);
        Task<List<BankAccount>> AddAccount(BankAccDTO bankAcc);
        Task<List<BankAccount>> DeleteAccount(int id);
        Task<BankAccount> GetAccountById(int id);
        Task<List<BankAccount>> GetAllAccounts();
    }
}
