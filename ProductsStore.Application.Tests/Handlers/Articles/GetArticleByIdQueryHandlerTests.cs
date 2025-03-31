using AutoMapper;

using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Articles.Queries;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Tests.Handlers.Articles
{
    public class GetArticleByIdQueryHandlerTests
    {
        private readonly Mock<IArticleRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task Handle_ReturnsArticleDetailsDTO()
        {
            // Arrange
            GetArticleByIdQuery query = new() { Id = 1 };
            Article article = new() { Id = 1, Name = "Test" };
            ArticleDetailsDTO expected = new() { Id = 1, Name = "Test" };

            _ = _repositoryMock.Setup(r => r.GetByIdWithCommentsAsync(1)).ReturnsAsync(article);
            _ = _mapperMock.Setup(m => m.Map<ArticleDetailsDTO>(article)).Returns(expected);

            GetArticleByIdQueryHandler handler = new(_repositoryMock.Object, _mapperMock.Object);

            // Act
            ArticleDetailsDTO result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Id);
        }
    }

}
