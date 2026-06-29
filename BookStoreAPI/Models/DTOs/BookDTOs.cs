namespace BookStoreAPI.Models.DTOs.Books;

public class BookResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime PublishedDate { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime PublishedDate { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}

public class UpdateBookRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime PublishedDate { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}

public class BookQueryParams
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? AuthorId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}
