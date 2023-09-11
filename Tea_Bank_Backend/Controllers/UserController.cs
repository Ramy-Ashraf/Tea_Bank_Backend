﻿using Microsoft.AspNetCore.Authorization;
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

            //user.Email = request.Email;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;
            
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


        //private RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        Expires = DateTime.Now.AddDays(7),
        //        Created = DateTime.Now
        //    };

        //    return refreshToken;
        //}

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

        //private string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.Role, "Admin"),
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: creds);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}

    }
}