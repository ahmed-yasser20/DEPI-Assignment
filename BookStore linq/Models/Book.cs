namespace BookStoreDataLayer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    }
}
