using AutoMapper;

using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Articles.Commands;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Tests.Handlers.Articles
{
    public class UpdateArticleCommandHandlerTests
    {
        private readonly Mock<IArticleRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IArticleService> _articleServiceMock = new();

        [Fact]
        public async Task Handle_UpdatesArticle_ReturnsUpdatedDTO()
        {
            // Arrange
            UpdateArticleCommand command = new()
            {
                ArticleId = 1,
                ArticleDTO = new CreateArticleDTO
                {
                    Name = "Updated",
                    Description = "New Desc",
                    AuthorId = "user1"
                }
            };

            Article article = new() { Id = 1, Name = "Old" };
            ArticleDTO articleDto = new() { Id = 1, Name = "Updated" };

            _ = _repositoryMock.Setup(r => r.GetByIdWithCommentsAsync(1)).ReturnsAsync(article);
            _mapperMock.Setup(m => m.Map(command.ArticleDTO, article)).Verifiable();
            _ = _mapperMock.Setup(m => m.Map<ArticleDTO>(article)).Returns(articleDto);
            _articleServiceMock.Setup(s => s.UserCanManageArticle(It.IsAny<int>(),It.IsAny<string>())).ReturnsAsync(true);

            UpdateArticleCommandHandler handler = new(_repositoryMock.Object, _mapperMock.Object, _articleServiceMock.Object);

            // Act
            ArticleDTO result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(article), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.Equal("Updated", result.Name);
        }
    }

}
