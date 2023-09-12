using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using tea_bank.DTOs;
using tea_bank.Services;
using Tea_Bank_Backend.DTOs;
using Tea_Bank_Backend.Models;
using Tea_Bank_Backend.Services;

namespace Tea_Bank_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAccountController : ControllerBase
    {
        public static User user = new User();
        private readonly IUserService _userService;
        private readonly IAuthService _AuthService;
        private readonly ApplicationDbContext _context;

        public MyAccountController(IUserService userService, IAuthService authService, ApplicationDbContext context)
        {
            _userService = userService;
            _AuthService = authService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            _AuthService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var request2 = await _userService.AddUser(request);

            request2.Add(new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            return Ok(request2);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO request)
        {
            // search for user with email and verify password
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user is null || !_AuthService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid email or password.");
            }

            string token = _AuthService.CreateToken(user);

            var refreshToken = _AuthService.GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        // get current logged in user
        [HttpGet("current"), Authorize]

        public async Task<ActionResult<User>> CurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
            {
                return NotFound("User not Found.");
            }

            user.BankAccounts = _context.BankAccounts.Where(b => b.User.Id == user.Id).ToList();
            user.Reservations = _context.Reservations.Where(r => r.User.Id == user.Id).ToList();

            return Ok(user);
        }

        // update current logged in user
        [HttpPut("current"), Authorize]
        public async Task<ActionResult<User>> UpdateCurrentUser(UserDTO user)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _userService.UpdateCurrentUser(email, user);
            if (result is null)
            {
                return NotFound("User not Found.");
            }

            return Ok(result);
        }

        // delete current logged in user
        [HttpDelete("current"), Authorize]
        public async Task<ActionResult<User>> DeleteCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _userService.DeleteCurrentUser(email);
            if (result is null)
            {
                return NotFound("User not Found.");
            }

            return Ok(result);
        }

        // get current logged in user's bank accounts
        [HttpGet("current/bankaccounts"), Authorize]
        public async Task<ActionResult<List<BankAccount>>> GetCurrentUserBankAccounts()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _userService.GetCurrentUserBankAccounts(email);
            if (result is null)
            {
                return NotFound("User not Found.");
            }

            return Ok(result);
        }
        // add bank account to current logged in user
        //[HttpPost("current/bankaccounts"), Authorize]
        //public async Task<ActionResult<List<BankAccount>>> AddAccount(BankAccDTO bankAcc)
        //{
        //    var email = User.FindFirst(ClaimTypes.Email)?.Value;
        //    var result = await _userService.AddAccount(email, bankAcc);
        //    if (result is null)
        //    {
        //        return NotFound("User not Found.");
        //    }

        //    return Ok(result);
        //}
    }
}
