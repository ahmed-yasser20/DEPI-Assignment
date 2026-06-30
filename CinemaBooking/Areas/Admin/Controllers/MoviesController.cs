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
    public class MoviesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public MoviesController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies.OrderByDescending(m => m.ReleaseDate).ToListAsync();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create() => View(new MovieFormViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string? posterPath = null;

            if (model.Poster != null && model.Poster.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Poster.FileName);
                var savePath = Path.Combine(_env.WebRootPath, "uploads", "posters", fileName);
                using var stream = new FileStream(savePath, FileMode.Create);
                await model.Poster.CopyToAsync(stream);
                posterPath = "/uploads/posters/" + fileName;
            }

            var movie = new Movie
            {
                Title = model.Title,
                Description = model.Description,
                Genre = model.Genre,
                DurationMinutes = model.DurationMinutes,
                ReleaseDate = model.ReleaseDate,
                PosterPath = posterPath
            };

            _db.Movies.Add(movie);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Movie added successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            return View(new MovieFormViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate,
                ExistingPosterPath = movie.PosterPath
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var movie = await _db.Movies.FindAsync(model.Id);
            if (movie == null) return NotFound();

            if (model.Poster != null && model.Poster.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Poster.FileName);
                var savePath = Path.Combine(_env.WebRootPath, "uploads", "posters", fileName);
                using var stream = new FileStream(savePath, FileMode.Create);
                await model.Poster.CopyToAsync(stream);
                movie.PosterPath = "/uploads/posters/" + fileName;
            }

            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.Genre = model.Genre;
            movie.DurationMinutes = model.DurationMinutes;
            movie.ReleaseDate = model.ReleaseDate;

            await _db.SaveChangesAsync();

            TempData["Success"] = "Movie updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Movie deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
