namespace CinemaBooking.Models.Entities
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public int BookedSeats => Bookings.Sum(b => b.NumberOfSeats);
        public int AvailableSeats => Hall != null ? Hall.Capacity - BookedSeats : 0;
        public bool HasStarted => StartTime <= DateTime.Now;
    }
}
