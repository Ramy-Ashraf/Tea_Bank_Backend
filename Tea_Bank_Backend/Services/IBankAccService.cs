using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Models;

namespace Tea_Bank_Backend.Services
{
    public interface IBankAccService
    {
        
        Task<List<BankAccount>> DeleteAccount(int id);
        Task<BankAccount> GetAccountById(int id);
        Task<List<BankAccount>> GetAllAccounts();
    }
}
