namespace ProductsStore.Application.DTOs
{
    public class CreateArticleDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public  string? AuthorId { get; set; }
    }
}
