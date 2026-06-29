using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _db;

        public AuthorService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            var authors = await _db.Authors.ToListAsync();

            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio
            }).ToList();
        }

        public async Task<AuthorDto?> GetById(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio
            };
        }

        public async Task<AuthorDto> Create(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Name = dto.Name,
                Bio = dto.Bio
            };

            _db.Authors.Add(author);
            await _db.SaveChangesAsync();

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio
            };
        }

        public async Task<AuthorDto?> Update(int id, CreateAuthorDto dto)
        {
            var author = await _db.Authors.FindAsync(id);
            if (author == null) return null;

            author.Name = dto.Name;
            author.Bio = dto.Bio;

            await _db.SaveChangesAsync();

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio
            };
        }

        public async Task<bool> Delete(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            if (author == null) return false;

            var hasBooks = await _db.Books.AnyAsync(b => b.AuthorId == id);
            if (hasBooks)
            {
                throw new Exception("Cannot delete an author who has books.");
            }

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
