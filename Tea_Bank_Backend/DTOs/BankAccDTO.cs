using tea_bank.Models;
using System.Text.Json.Serialization; //3ashan jsonIgnore teshtaghal
using System.ComponentModel.DataAnnotations.Schema;

namespace tea_bank.DTOs
{
    public class BankAccDTO
    {
        public int Id { get; set; }

        //public int CustomerId { get; set; }

        public long Balance { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }

        
        public int UserId { get; set; }

    }
}
