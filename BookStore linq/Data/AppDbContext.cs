using BookStoreDataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDataLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>(b =>
            {
                b.Property(x => x.Price).HasColumnType("decimal(18,2)");
                b.HasCheckConstraint("CK_Book_Price", "[Price] >= 0");

                b.HasOne(x => x.Author)
                 .WithMany(a => a.Books)
                 .HasForeignKey(x => x.AuthorId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Category)
                 .WithMany(c => c.Books)
                 .HasForeignKey(x => x.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            builder.Entity<Purchase>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PurchaseItem>(pi =>
            {
                pi.Property(x => x.PriceAtPurchase).HasColumnType("decimal(18,2)");

                pi.HasOne(x => x.Purchase)
                  .WithMany(p => p.Items)
                  .HasForeignKey(x => x.PurchaseId)
                  .OnDelete(DeleteBehavior.Cascade);

                pi.HasOne(x => x.Book)
                  .WithMany(b => b.PurchaseItems)
                  .HasForeignKey(x => x.BookId)
                  .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
