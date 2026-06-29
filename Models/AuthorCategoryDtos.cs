namespace BookStoreAPI.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Bio { get; set; }
    }

    public class CreateAuthorDto
    {
        public string Name { get; set; } = "";
        public string? Bio { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
