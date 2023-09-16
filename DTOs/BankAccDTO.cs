using tea_bank.Models;
using System.Text.Json.Serialization; //3ashan jsonIgnore teshtaghal
using System.ComponentModel.DataAnnotations.Schema;

namespace tea_bank.DTOs
{
    public class BankAccDTO
    {
        public int Id { get; set; }

        public DateTime DateOfOPening { get; set; } = DateTime.Now;

        public int Balance { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }
       
    }

}
