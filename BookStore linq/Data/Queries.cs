using BookStoreDataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDataLayer.Data
{
    public class Queries
    {
        private readonly AppDbContext _db;

        public Queries(AppDbContext db)
        {
            _db = db;
        }

        public List<Book> GetAllBooksWithDetails()
        {
            return _db.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToList();
        }

        public List<(Book Book, int TotalSold)> GetTopSellingBooks(int count = 5)
        {
            return _db.PurchaseItems
                .AsNoTracking()
                .GroupBy(pi => pi.Book)
                .Select(g => new { Book = g.Key, TotalSold = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.TotalSold)
                .Take(count)
                .ToList()
                .Select(x => (x.Book, x.TotalSold))
                .ToList();
        }

        public List<(Customer Customer, int PurchaseCount)> GetCustomersWithPurchaseCount()
        {
            return _db.Customers
                .AsNoTracking()
                .Select(c => new { Customer = c, PurchaseCount = c.Purchases.Count })
                .ToList()
                .Select(x => (x.Customer, x.PurchaseCount))
                .ToList();
        }

        public List<Category> GetCategoriesWithMoreThanXBooks(int minBooks = 5)
        {
            return _db.Categories
                .AsNoTracking()
                .Where(c => c.Books.Count > minBooks)
                .ToList();
        }

        public List<Book> GetBooksAboveAveragePrice()
        {
            var averagePrice = _db.Books.Average(b => b.Price);

            return _db.Books
                .AsNoTracking()
                .Where(b => b.Price > averagePrice)
                .ToList();
        }

        public List<Customer> GetCustomersWithNoPurchases()
        {
            return _db.Customers
                .AsNoTracking()
                .Where(c => !c.Purchases.Any())
                .ToList();
        }

        public List<(int Year, int Month, decimal TotalRevenue)> GetRevenueByMonth()
        {
            return _db.PurchaseItems
                .AsNoTracking()
                .Select(pi => new
                {
                    pi.Purchase.PurchaseDate.Year,
                    pi.Purchase.PurchaseDate.Month,
                    Total = pi.Quantity * pi.PriceAtPurchase
                })
                .GroupBy(x => new { x.Year, x.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Total = g.Sum(x => x.Total) })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList()
                .Select(x => (x.Year, x.Month, x.Total))
                .ToList();
        }

        public List<Book> SearchBooksByTitle(string keyword)
        {
            return _db.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Where(b => EF.Functions.Like(b.Title, $"%{keyword}%"))
                .ToList();
        }

        public List<Book> GetBooksPaged(int pageNumber, int pageSize)
        {
            return _db.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Category)
                .OrderBy(b => b.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Book AddBook(string title, decimal price, int stock, int authorId, int? categoryId)
        {
            var book = new Book
            {
                Title = title,
                Price = price,
                Stock = stock,
                AuthorId = authorId,
                CategoryId = categoryId
            };

            _db.Books.Add(book);
            _db.SaveChanges();
            return book;
        }

        public bool UpdateBookPrice(int bookId, decimal newPrice)
        {
            var book = _db.Books.Find(bookId);
            if (book == null) return false;

            book.Price = newPrice;
            _db.SaveChanges();
            return true;
        }

        public bool DeleteBook(int bookId)
        {
            var book = _db.Books.Find(bookId);
            if (book == null) return false;

            _db.Books.Remove(book);
            _db.SaveChanges();
            return true;
        }
    }
}
