using CinemaBooking.Data;
using CinemaBooking.Models.Entities;
using CinemaBooking.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public BookingsController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var bookings = await _db.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Hall)
                        .ThenInclude(h => h.Cinema)
                .OrderByDescending(b => b.BookedAt)
                .ToListAsync();

            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Book(int showtimeId)
        {
            var showtime = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                    .ThenInclude(h => h.Cinema)
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null) return NotFound();

            if (showtime.HasStarted)
            {
                TempData["Error"] = "This showtime has already started.";
                return RedirectToAction("Details", "Movies", new { id = showtime.MovieId });
            }

            ViewBag.Showtime = showtime;
            return View(new BookingViewModel { ShowtimeId = showtimeId });
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookingViewModel model)
        {
            var showtime = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                    .ThenInclude(h => h.Cinema)
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.Id == model.ShowtimeId);

            if (showtime == null) return NotFound();

            ViewBag.Showtime = showtime;

            if (!ModelState.IsValid) return View(model);

            if (showtime.HasStarted)
            {
                TempData["Error"] = "This showtime has already started.";
                return RedirectToAction("Details", "Movies", new { id = showtime.MovieId });
            }

            if (model.NumberOfSeats > showtime.AvailableSeats)
            {
                ModelState.AddModelError("NumberOfSeats", $"Only {showtime.AvailableSeats} seats available.");
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            var booking = new Booking
            {
                UserId = userId!,
                ShowtimeId = model.ShowtimeId,
                NumberOfSeats = model.NumberOfSeats,
                TotalPrice = model.NumberOfSeats * showtime.TicketPrice
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Booking confirmed successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);

            var booking = await _db.Bookings
                .Include(b => b.Showtime)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (booking == null) return NotFound();

            if (booking.Showtime.HasStarted)
            {
                TempData["Error"] = "You cannot cancel a booking after the showtime has started.";
                return RedirectToAction("Index");
            }

            _db.Bookings.Remove(booking);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Booking cancelled successfully.";
            return RedirectToAction("Index");
        }
    }
}
