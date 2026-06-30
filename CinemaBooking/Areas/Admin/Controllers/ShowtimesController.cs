using CinemaBooking.Data;
using CinemaBooking.Models.Entities;
using CinemaBooking.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShowtimesController : Controller
    {
        private readonly AppDbContext _db;

        public ShowtimesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var showtimes = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall).ThenInclude(h => h.Cinema)
                .Include(s => s.Bookings)
                .OrderBy(s => s.StartTime)
                .ToListAsync();

            return View(showtimes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Movies = new SelectList(await _db.Movies.ToListAsync(), "Id", "Title");
            ViewBag.Halls = new SelectList(await _db.Halls.Include(h => h.Cinema).ToListAsync(), "Id", "Name");
            return View(new ShowtimeFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShowtimeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Movies = new SelectList(await _db.Movies.ToListAsync(), "Id", "Title");
                ViewBag.Halls = new SelectList(await _db.Halls.Include(h => h.Cinema).ToListAsync(), "Id", "Name");
                return View(model);
            }

            _db.Showtimes.Add(new Showtime
            {
                StartTime = model.StartTime,
                TicketPrice = model.TicketPrice,
                MovieId = model.MovieId,
                HallId = model.HallId
            });

            await _db.SaveChangesAsync();

            TempData["Success"] = "Showtime added successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var showtime = await _db.Showtimes.FindAsync(id);
            if (showtime == null) return NotFound();

            ViewBag.Movies = new SelectList(await _db.Movies.ToListAsync(), "Id", "Title", showtime.MovieId);
            ViewBag.Halls = new SelectList(await _db.Halls.Include(h => h.Cinema).ToListAsync(), "Id", "Name", showtime.HallId);

            return View(new ShowtimeFormViewModel
            {
                Id = showtime.Id,
                StartTime = showtime.StartTime,
                TicketPrice = showtime.TicketPrice,
                MovieId = showtime.MovieId,
                HallId = showtime.HallId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShowtimeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Movies = new SelectList(await _db.Movies.ToListAsync(), "Id", "Title");
                ViewBag.Halls = new SelectList(await _db.Halls.Include(h => h.Cinema).ToListAsync(), "Id", "Name");
                return View(model);
            }

            var showtime = await _db.Showtimes.FindAsync(model.Id);
            if (showtime == null) return NotFound();

            showtime.StartTime = model.StartTime;
            showtime.TicketPrice = model.TicketPrice;
            showtime.MovieId = model.MovieId;
            showtime.HallId = model.HallId;

            await _db.SaveChangesAsync();

            TempData["Success"] = "Showtime updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var showtime = await _db.Showtimes.FindAsync(id);
            if (showtime == null) return NotFound();

            _db.Showtimes.Remove(showtime);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Showtime deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
