using CinemaBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies
                .Include(m => m.Showtimes)
                .OrderByDescending(m => m.ReleaseDate)
                .Take(6)
                .ToListAsync();

            return View(movies);
        }

        public IActionResult Error404() => View();
        public IActionResult Error500() => View();
    }
}
