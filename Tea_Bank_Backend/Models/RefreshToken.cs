using System.ComponentModel.DataAnnotations;

namespace Tea_Bank_Backend.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}