namespace ProductsStore.Application.DTOs
{
    public class AddCommentDTO
    {
        public required string Text { get; set; }
        public string? UserId { get; set; }
    }
}
