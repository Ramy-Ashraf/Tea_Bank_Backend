using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tea_bank.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public string Services { get; set; }

        public DateTime TimeSlot { get; set; } // .ToString("HH:mm")

        public DateTime Date { get; set; } // .ToString("MM/dd/yyyy")

        // add UserId as forign Key to User table
        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        
    }
}
