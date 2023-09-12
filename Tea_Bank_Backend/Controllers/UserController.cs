using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using tea_bank.DTOs;
using tea_bank.Services;
using Tea_Bank_Backend.Models;
using Tea_Bank_Backend.DTOs;
using Tea_Bank_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace tea_bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IAuthService _AuthService;
        private readonly ApplicationDbContext _context;

        public UserController(IConfiguration configuration, IUserService userService, IAuthService authService, ApplicationDbContext context)
        {
            _userService = userService;
            _AuthService = authService;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }
        [HttpPost]
        public async Task<ActionResult<List<BankAccount>>> AddAccount(int id, BankAccDTO bankAcc)
        {
            return await _userService.AddAccount(id, bankAcc);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result is null)
            {
                return NotFound("User not Found.");
            }

            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<User>>> AddUser(UserDTO user)
        {
            var result = await _userService.AddUser(user);

            return Ok(result);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, UserDTO user)
        {
            var result = await _userService.UpdateUser(id, user);
            if (result is null)
            {
                return NotFound("User not Found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
            {
                return NotFound("User not Found.");
            }

            return Ok(result);
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

            //// if email already exists
            //if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            //{
            //    return BadRequest("Email already Used.");
            //}

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
    }
}