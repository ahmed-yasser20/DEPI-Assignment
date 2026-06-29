namespace BookStoreAPI.Models
{
    public class CreateOrderDto
    {
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderResultDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "";
        public List<OrderItemResultDto> Items { get; set; } = new List<OrderItemResultDto>();
    }

    public class OrderItemResultDto
    {
        public string BookTitle { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
