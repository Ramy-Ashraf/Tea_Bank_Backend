using tea_bank.Models;

namespace tea_bank.DTOs
{
    public class UserDTO
    {

        public int Id { get; set; }

        public int NationalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; } = string.Empty;

        public List<BankAccDTO> BankAccounts { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
