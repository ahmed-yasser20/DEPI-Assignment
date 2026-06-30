using CinemaBooking.Data;
using CinemaBooking.Models.Entities;
using CinemaBooking.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CinemasController : Controller
    {
        private readonly AppDbContext _db;

        public CinemasController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.Cinemas.Include(c => c.Halls).ToListAsync();
            return View(cinemas);
        }

        [HttpGet]
        public IActionResult Create() => View(new CinemaFormViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(CinemaFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Cinemas.Add(new Cinema { Name = model.Name, Location = model.Location });
            await _db.SaveChangesAsync();

            TempData["Success"] = "Cinema added successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _db.Cinemas.FindAsync(id);
            if (cinema == null) return NotFound();

            return View(new CinemaFormViewModel { Id = cinema.Id, Name = cinema.Name, Location = cinema.Location });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CinemaFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cinema = await _db.Cinemas.FindAsync(model.Id);
            if (cinema == null) return NotFound();

            cinema.Name = model.Name;
            cinema.Location = model.Location;
            await _db.SaveChangesAsync();

            TempData["Success"] = "Cinema updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _db.Cinemas.FindAsync(id);
            if (cinema == null) return NotFound();

            _db.Cinemas.Remove(cinema);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Cinema deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
