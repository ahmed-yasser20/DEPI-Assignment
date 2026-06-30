using BookStoreDataLayer.Models;

namespace BookStoreDataLayer.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Books.Any()) return;

            var fiction = new Category { Name = "Fiction" };
            var science = new Category { Name = "Science" };
            var history = new Category { Name = "History" };

            var author1 = new Author { Name = "George Orwell" };
            var author2 = new Author { Name = "Isaac Asimov" };
            var author3 = new Author { Name = "Yuval Noah Harari" };

            var books = new List<Book>
            {
                new Book { Title = "1984", Price = 15.99m, Stock = 50, Author = author1, Category = fiction },
                new Book { Title = "Animal Farm", Price = 12.50m, Stock = 30, Author = author1, Category = fiction },
                new Book { Title = "Foundation", Price = 18.00m, Stock = 25, Author = author2, Category = science },
                new Book { Title = "I, Robot", Price = 14.75m, Stock = 40, Author = author2, Category = science },
                new Book { Title = "Sapiens", Price = 22.00m, Stock = 60, Author = author3, Category = history },
                new Book { Title = "Homo Deus", Price = 21.50m, Stock = 35, Author = author3, Category = history }
            };

            var customers = new List<Customer>
            {
                new Customer { Name = "Ahmed Yasser", Email = "ahmed@example.com" },
                new Customer { Name = "Sara Mostafa", Email = "sara@example.com" },
                new Customer { Name = "Omar Khaled", Email = "omar@example.com" }
            };

            db.Categories.AddRange(fiction, science, history);
            db.Authors.AddRange(author1, author2, author3);
            db.Books.AddRange(books);
            db.Customers.AddRange(customers);
            db.SaveChanges();

            var purchase1 = new Purchase
            {
                Customer = customers[0],
                PurchaseDate = DateTime.Now.AddMonths(-2),
                Items = new List<PurchaseItem>
                {
                    new PurchaseItem { Book = books[0], Quantity = 2, PriceAtPurchase = books[0].Price },
                    new PurchaseItem { Book = books[2], Quantity = 1, PriceAtPurchase = books[2].Price }
                }
            };

            var purchase2 = new Purchase
            {
                Customer = customers[0],
                PurchaseDate = DateTime.Now.AddMonths(-1),
                Items = new List<PurchaseItem>
                {
                    new PurchaseItem { Book = books[4], Quantity = 1, PriceAtPurchase = books[4].Price }
                }
            };

            var purchase3 = new Purchase
            {
                Customer = customers[1],
                PurchaseDate = DateTime.Now,
                Items = new List<PurchaseItem>
                {
                    new PurchaseItem { Book = books[0], Quantity = 1, PriceAtPurchase = books[0].Price },
                    new PurchaseItem { Book = books[1], Quantity = 3, PriceAtPurchase = books[1].Price },
                    new PurchaseItem { Book = books[5], Quantity = 1, PriceAtPurchase = books[5].Price }
                }
            };

            db.Purchases.AddRange(purchase1, purchase2, purchase3);
            db.SaveChanges();
        }
    }
}
