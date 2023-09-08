using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tea_bank.Data;
using tea_bank.DTOs;
using AutoMapper;
using System.Security.Cryptography;
using Tea_Bank_Backend.DTOs;
using tea_bank.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tea_Bank_Backend.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Reservation>> ADDReservation(ReservationDTO reservation)
        {
            var newReservation = new Reservation
            {
                Services = reservation.Services,
                TimeSlot = reservation.TimeSlot,
                Date = reservation.Date

            };
            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();

            return await _context.Reservations.ToListAsync();
        }
        public async Task<List<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Guests.FindAsync(id);
            if (reservation == null)
            {
                return null;
            }
            _context.Guests.Remove(reservation);
            await _context.SaveChangesAsync();

            return await _context.Reservations.ToListAsync();
        }

        public async Task<List<Reservation>> GetALLReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return reservations;
        }
        public async Task<Reservation> GetReservationByID(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return null;
            }

            return reservation;
        }





    }

}

