using BookStoreDataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);

using var db = new AppDbContext(optionsBuilder.Options);

db.Database.Migrate();
DbSeeder.Seed(db);

var queries = new Queries(db);

Console.WriteLine(" All Books With Author and Category");
foreach (var book in queries.GetAllBooksWithDetails())
{
    Console.WriteLine($"{book.Title} | {book.Author.Name} | {book.Category?.Name ?? "No Category"} | ${book.Price}");
}

Console.WriteLine("\n Top 5 Best Selling Books ");
foreach (var (book, totalSold) in queries.GetTopSellingBooks())
{
    Console.WriteLine($"{book.Title} - Sold: {totalSold}");
}

Console.WriteLine("\n Customers With Purchase Count");
foreach (var (customer, count) in queries.GetCustomersWithPurchaseCount())
{
    Console.WriteLine($"{customer.Name} - Purchases: {count}");
}

Console.WriteLine("\n Categories With More Than 5 Books ");
var bigCategories = queries.GetCategoriesWithMoreThanXBooks(0);
foreach (var category in bigCategories)
{
    Console.WriteLine($"{category.Name} - Books: {category.Books.Count}");
}

Console.WriteLine("\n Books Above Average Price ");
foreach (var book in queries.GetBooksAboveAveragePrice())
{
    Console.WriteLine($"{book.Title} - ${book.Price}");
}

Console.WriteLine("\n Customers With No Purchases ");
var inactiveCustomers = queries.GetCustomersWithNoPurchases();
if (!inactiveCustomers.Any())
    Console.WriteLine("None - all customers have made a purchase");
foreach (var customer in inactiveCustomers)
{
    Console.WriteLine($"{customer.Name} - {customer.Email}");
}

Console.WriteLine("\n Revenue By Month ");
foreach (var (year, month, total) in queries.GetRevenueByMonth())
{
    Console.WriteLine($"{year}-{month:D2}: ${total}");
}

Console.WriteLine("\n Search Books by Keyword 'a' ");
foreach (var book in queries.SearchBooksByTitle("a"))
{
    Console.WriteLine($"{book.Title} - {book.Author.Name}");
}

Console.WriteLine("\n Books Page 1 (Page Size 3) ");
foreach (var book in queries.GetBooksPaged(1, 3))
{
    Console.WriteLine($"{book.Title}");
}

Console.WriteLine("\n Add, Update, Delete Book Demo ");
var newBook = queries.AddBook("Brave New World", 16.00m, 20, db.Authors.First().Id, db.Categories.First().Id);
Console.WriteLine($"Added: {newBook.Title} - ${newBook.Price}");

queries.UpdateBookPrice(newBook.Id, 17.50m);
Console.WriteLine($"Updated price to: ${db.Books.Find(newBook.Id)!.Price}");

queries.DeleteBook(newBook.Id);
Console.WriteLine("Book deleted");

Console.WriteLine("\nDone.");
