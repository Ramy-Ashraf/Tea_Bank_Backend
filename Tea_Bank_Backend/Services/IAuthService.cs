using Microsoft.AspNetCore.Mvc;
using tea_bank.DTOs;
using Tea_Bank_Backend.DTOs;
using Tea_Bank_Backend.Models;

namespace Tea_Bank_Backend.Services
{
    public interface IAuthService
    {
        RefreshToken GenerateRefreshToken();
        string CreateToken(User user);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
