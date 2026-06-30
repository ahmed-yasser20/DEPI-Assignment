using CinemaBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext _db;

        public CinemasController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.Cinemas
                .Include(c => c.Halls)
                .ToListAsync();

            return View(cinemas);
        }
    }
}
