namespace BookStoreDataLayer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
