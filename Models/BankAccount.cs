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
        
        public int Balance { get; set; }
        
        public string Currency { get; set; }
        
        public string Type { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
