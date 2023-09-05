using System.ComponentModel.DataAnnotations;

namespace tea_bank.Models
{
    public class Guest
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string Message { get; set; }
    }
}
