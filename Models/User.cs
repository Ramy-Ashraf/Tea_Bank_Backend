using System.ComponentModel.DataAnnotations;

namespace tea_bank.Models
{
    public class User
    {
        
        public int Id { get; set; }
        
        public int NationalId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public List<BankAccount> BankAccounts { get; set; }

        public List<Reservation>? Reservations { get; set; }
        
        public DateTime DateOfJoin { get; set;} = DateTime.Now;
    }
}
