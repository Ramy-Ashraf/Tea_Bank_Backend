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

    }
}