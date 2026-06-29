using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _db;

        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _db.Categories.ToListAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDto?> GetById(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null) return null;

            return new CategoryDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };
        }

        public async Task<CategoryDto> Create(CreateCategoryDto dto)
        {
            var cat = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _db.Categories.Add(cat);
            await _db.SaveChangesAsync();

            return new CategoryDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };
        }

        public async Task<CategoryDto?> Update(int id, CreateCategoryDto dto)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null) return null;

            cat.Name = dto.Name;
            cat.Description = dto.Description;

            await _db.SaveChangesAsync();

            return new CategoryDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };
        }

        public async Task<bool> Delete(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null) return false;

            var hasBooks = await _db.Books.AnyAsync(b => b.CategoryId == id);
            if (hasBooks)
            {
                throw new Exception("Cannot delete a category that contains books.");
            }

            _db.Categories.Remove(cat);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
