using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class BookService
    {
        private readonly AppDbContext _db;

        public BookService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<BookDto>> GetAllBooks(string? search, int? categoryId, int? authorId, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            var query = _db.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.ISBN.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }

            var skip = (page - 1) * pageSize;
            var books = await query.Skip(skip).Take(pageSize).ToListAsync();

            var result = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ISBN = b.ISBN,
                Price = b.Price,
                Stock = b.Stock,
                PublishedDate = b.PublishedDate,
                AuthorName = b.Author != null ? b.Author.Name : "",
                CategoryName = b.Category != null ? b.Category.Name : ""
            }).ToList();

            return result;
        }

        public async Task<BookDto?> GetBookById(int id)
        {
            var book = await _db.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Price = book.Price,
                Stock = book.Stock,
                PublishedDate = book.PublishedDate,
                AuthorName = book.Author != null ? book.Author.Name : "",
                CategoryName = book.Category != null ? book.Category.Name : ""
            };
        }

        public async Task<BookDto> CreateBook(CreateBookDto dto)
        {
            var author = await _db.Authors.FindAsync(dto.AuthorId);
            if (author == null)
            {
                throw new Exception("Author not found.");
            }

            var category = await _db.Categories.FindAsync(dto.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                ISBN = dto.ISBN,
                Price = dto.Price,
                Stock = dto.Stock,
                PublishedDate = dto.PublishedDate,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId
            };

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Price = book.Price,
                Stock = book.Stock,
                PublishedDate = book.PublishedDate,
                AuthorName = author.Name,
                CategoryName = category.Name
            };
        }

        public async Task<BookDto?> UpdateBook(int id, UpdateBookDto dto)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return null;

            var author = await _db.Authors.FindAsync(dto.AuthorId);
            if (author == null) throw new Exception("Author not found.");

            var category = await _db.Categories.FindAsync(dto.CategoryId);
            if (category == null) throw new Exception("Category not found.");

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.ISBN = dto.ISBN;
            book.Price = dto.Price;
            book.Stock = dto.Stock;
            book.PublishedDate = dto.PublishedDate;
            book.AuthorId = dto.AuthorId;
            book.CategoryId = dto.CategoryId;

            await _db.SaveChangesAsync();

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Price = book.Price,
                Stock = book.Stock,
                PublishedDate = book.PublishedDate,
                AuthorName = author.Name,
                CategoryName = category.Name
            };
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return false;

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
