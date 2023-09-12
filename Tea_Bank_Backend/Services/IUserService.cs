using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using tea_bank.Models;

namespace tea_bank.Services
{
    public interface IUserService
    {
        Task<List<User>> AddUser(UserDTO user);
        Task<List<User>> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<List<User>> UpdateUser(int id, UserDTO user);
        Task<List<BankAccount>> AddAccount(int id, BankAccDTO bankAcc);
        
        Task<User> UpdateCurrentUser(string email, UserDTO user);
        Task<User> DeleteCurrentUser(string email);

    }
}
