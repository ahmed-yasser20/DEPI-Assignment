namespace BookStoreDataLayer.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
    }

    public class PurchaseItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
