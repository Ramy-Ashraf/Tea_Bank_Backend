using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tea_bank.Data;
using tea_bank.DTOs;

namespace tea_bank.Services
{
    public class UserService : IUserService
    {
        //private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            //_mapper = mapper;
            _context = context;
        }

        public async Task<List<User>> AddUser(UserDTO user)
        {
            var newUser = new User
            {
                //Id = user.Id,
                NationalId = user.NationalId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password
            };

            var bankAccount = user.BankAccounts.Select(b => new BankAccount
            {
                Id = b.Id,
                //CustomerId = b.CustomerId,
               // DateOfOPening = b.DateOfOPening,
                Balance = b.Balance,
                Currency = b.Currency,
                Type = b.Type,
                User = newUser
            }).ToList();

            var reservation = user.Reservations.Select(r => new Reservation
            {
                Id = r.Id,
                //CustomerId = r.CustomerId,
                Services = r.Services,
                TimeSlot = r.TimeSlot,
                Date = r.Date,
                User = newUser
            }).ToList();

            newUser.BankAccounts = bankAccount;
            newUser.Reservations = reservation;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return await _context.Users.Include(u => u.BankAccounts.Where(b => b.User.Id == user.Id)).Include(u => u.Reservations.Where(r => r.User.Id == user.Id)).ToListAsync();

        }

        public async Task<List<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                return null;
            }
            // TODO: add to deactivated accounts

            _context.Users.Remove(user);
            _context.BankAccounts.RemoveRange(_context.BankAccounts.Where(b => b.User.Id == user.Id));   
            _context.Reservations.RemoveRange(_context.Reservations.Where(r => r.User.Id == user.Id));
            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
            
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            users.ForEach(u => u.BankAccounts = _context.BankAccounts.Where(b => b.User.Id == u.Id).ToList());
            users.ForEach(u => u.Reservations = _context.Reservations.Where(r => r.User.Id == u.Id).ToList());
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            user.BankAccounts = _context.BankAccounts.Where(b => b.User.Id == user.Id).ToList();
            user.Reservations = _context.Reservations.Where(r => r.User.Id == user.Id).ToList();

            if (user is null)
            {
                return null;
            }

            return user;
        }

        public async Task<List<User>?> UpdateUser(int id, UserDTO user)
        {
            var userToUpdate = await _context.Users.FindAsync(id);
            if (userToUpdate is null)
            {
                return null;
            }

            //userToUpdate.Id = user.Id;
            userToUpdate.NationalId = user.NationalId;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.Password = user.Password;

            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
        }
    }
}