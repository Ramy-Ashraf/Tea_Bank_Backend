namespace Tea_Bank_Backend.DTOs
{
    public class ReservationDTO
    {

        public int Id { get; set; }

        public string Services { get; set; }

        public DateTime TimeSlot { get; set; } // .ToString("HH:mm")

        public DateTime Date { get; set; } // .ToString("MM/dd/yyyy")

        public int UserId { get; set; }
        
    }
}
