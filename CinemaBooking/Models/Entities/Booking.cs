namespace CinemaBooking.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; } = null!;
    }
}
