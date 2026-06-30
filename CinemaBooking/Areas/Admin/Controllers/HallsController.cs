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
    public class HallsController : Controller
    {
        private readonly AppDbContext _db;

        public HallsController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var halls = await _db.Halls.Include(h => h.Cinema).ToListAsync();
            return View(halls);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cinemas = new SelectList(await _db.Cinemas.ToListAsync(), "Id", "Name");
            return View(new HallFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(HallFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(await _db.Cinemas.ToListAsync(), "Id", "Name");
                return View(model);
            }

            _db.Halls.Add(new Hall { Name = model.Name, Capacity = model.Capacity, CinemaId = model.CinemaId });
            await _db.SaveChangesAsync();

            TempData["Success"] = "Hall added successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var hall = await _db.Halls.FindAsync(id);
            if (hall == null) return NotFound();

            ViewBag.Cinemas = new SelectList(await _db.Cinemas.ToListAsync(), "Id", "Name", hall.CinemaId);
            return View(new HallFormViewModel { Id = hall.Id, Name = hall.Name, Capacity = hall.Capacity, CinemaId = hall.CinemaId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HallFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(await _db.Cinemas.ToListAsync(), "Id", "Name");
                return View(model);
            }

            var hall = await _db.Halls.FindAsync(model.Id);
            if (hall == null) return NotFound();

            hall.Name = model.Name;
            hall.Capacity = model.Capacity;
            hall.CinemaId = model.CinemaId;
            await _db.SaveChangesAsync();

            TempData["Success"] = "Hall updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var hall = await _db.Halls.FindAsync(id);
            if (hall == null) return NotFound();

            _db.Halls.Remove(hall);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Hall deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
