using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.Features.Articles.Commands;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Tests.Handlers.Articles
{
    public class DeleteArticleCommandHandlerTests
    {
        private readonly Mock<IArticleRepository> _repositoryMock = new();
        private readonly Mock<IArticleService> _articleServiceMock = new();
        [Fact]
        public async Task Handle_DeletesArticle()
        {
            // Arrange
            DeleteArticleCommand command = new() { ArticleId = 1 };
            Article article = new() { Id = 1 };

            _ = _repositoryMock.Setup(r => r.GetByIdWithCommentsAsync(1)).ReturnsAsync(article);
            _articleServiceMock.Setup(s => s.UserCanManageArticle(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
            DeleteArticleCommandHandler handler = new(_repositoryMock.Object,_articleServiceMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Delete(article), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }

}
