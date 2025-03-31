using Microsoft.EntityFrameworkCore;
using ProductsStore.Domain.Entities;
using ProductsStore.Infraestructure.Persistence.Repositories;
using ProductsStore.Infraestructure.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Infraestruture.Tests.Repositories
{
    public class ArticleRepositoryTests
    {
        
        private DbContextOptions<ProductsStoreDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<ProductsStoreDbContext>()
                .UseInMemoryDatabase(databaseName: $"ArticlesDb_{System.Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public async Task AddAsync_AddsArticleToDatabase()
        {
            var options = GetInMemoryOptions();
            using var context = new ProductsStoreDbContext(options);
            var repo = new ArticleRepository(context);

            var article = new Article
            {
                Name = "Test Article",
                Description = "Description",
                AuthorId = "user1"
            };

            await repo.AddAsync(article);
            await repo.SaveChangesAsync();

            Assert.Single(context.Articles);
            Assert.Equal("Test Article", context.Articles.First().Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsPaginatedArticles()
        {
            var options = GetInMemoryOptions();
            using var context = new ProductsStoreDbContext(options);

            var user = new User { Id = "u1", Email = "author@test.com", UserName = "author" };
            context.Users.Add(user);
            context.Articles.AddRange(new[]
            {
            new Article { Name = "A1", Description = "D1",  AuthorId = "u1" },
            new Article { Name = "A2", Description = "D2", AuthorId = "u1" },
            new Article { Name = "A3", Description = "D3", AuthorId = "u1" }
        });

            await context.SaveChangesAsync();

            var repo = new ArticleRepository(context);
            var result = await repo.GetAllAsync(page: 1, pageSize: 2);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdWithCommentsAsync_ReturnsArticleWithCommentsAndAuthor()
        {
            var options = GetInMemoryOptions();
            using var context = new ProductsStoreDbContext(options);

            var user = new User { Id = "u1", Email = "author@test.com" };
            var article = new Article { Name = "Article 1", AuthorId = user.Id, Author = user,Description ="test" };
            var commentUser = new User { Id = "u2", Email = "commenter@test.com" };

            var comment = new Comment
            {
                Text = "Nice!",
                UserId = commentUser.Id,
                User = commentUser,
                Article = article
            };

            article.Comments = new List<Comment> { comment };

            context.Users.AddRange(user, commentUser);
            context.Articles.Add(article);
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            var repo = new ArticleRepository(context);
            var result = await repo.GetByIdWithCommentsAsync(article.Id);

            Assert.NotNull(result);
            Assert.NotNull(result!.Author);
            Assert.Single(result.Comments);
            Assert.Equal("Nice!", result.Comments.First().Text);
        }
    }
}
