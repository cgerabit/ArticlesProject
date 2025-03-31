namespace ProductsStore.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }

        public Article? Article { get; set; }

        public  User? User { get; set; }
        public required string UserId { get; set; }

        public required string Text { get; set; }
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    }
}
