using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tea_bank.Models
{
    public class BankAccount
    {
        
        public int Id { get; set; }
        
        //public int CustomerId { get; set; }
        
        public DateTime DateOfOPening { get; set; }
        
        public long Balance { get; set; }
        
        public string Currency { get; set; }
        
        public string Type { get; set; }

        // add UserId as forign Key to User table
        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
