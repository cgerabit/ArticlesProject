using AutoMapper;

using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Articles.Queries;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Tests.Handlers.Articles
{
    public class GetArticlesQueryHandlerTests
    {
        private readonly Mock<IArticleRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task Handle_ReturnsPaginatedArticles()
        {
            // Arrange
            GetArticlesQuery query = new() { Page = 1, PageSize = 10 };
            List<Article> articles = new()
            { new Article { Id = 1, Name = "Test" } };
            List<ArticleDTO> dtoList = new()
            { new ArticleDTO { Id = 1, Name = "Test" } };

            _ = _repositoryMock.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(articles);
            _ = _mapperMock.Setup(m => m.Map<IEnumerable<ArticleDTO>>(articles)).Returns(dtoList);

            GetArticlesQueryHandler handler = new(_repositoryMock.Object, _mapperMock.Object);

            // Act
            IEnumerable<ArticleDTO> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            _ = Assert.Single(result);
        }
    }

}
