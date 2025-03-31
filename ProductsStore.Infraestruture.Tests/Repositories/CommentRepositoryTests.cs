using Microsoft.EntityFrameworkCore;

using ProductsStore.Domain.Entities;
using ProductsStore.Infraestructure.Persistence;
using ProductsStore.Infraestructure.Persistence.Repositories;

namespace ProductsStore.Infraestruture.Tests.Repositories
{
    public class CommentRepositoryTests
    {
        private DbContextOptions<ProductsStoreDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<ProductsStoreDbContext>()
                .UseInMemoryDatabase(databaseName: $"CommentsDb_{System.Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public async Task AddAsync_AddsCommentToDatabase()
        {
            DbContextOptions<ProductsStoreDbContext> options = GetInMemoryOptions();
            using ProductsStoreDbContext context = new(options);
            CommentRepository repo = new(context);

            User user = new() { Id = "u1", Email = "user@test.com" };
            Article article = new() { Id = 1, Name = "A", Description = "D", AuthorId = "u1" };
            Comment comment = new() { ArticleId = article.Id, Text = "Great!", UserId = user.Id, User = user };

            _ = context.Users.Add(user);
            _ = context.Articles.Add(article);
            _ = await context.SaveChangesAsync();

            await repo.AddAsync(comment);
            await repo.SaveChangesAsync();

            _ = Assert.Single(context.Comments);
            Assert.Equal("Great!", context.Comments.First().Text);
        }
    }
}
