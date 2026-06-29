namespace BookStoreAPI.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string ISBN { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime PublishedDate { get; set; }
        public string AuthorName { get; set; } = "";
        public string CategoryName { get; set; } = "";
    }

    public class CreateBookDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string ISBN { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateBookDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string ISBN { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
