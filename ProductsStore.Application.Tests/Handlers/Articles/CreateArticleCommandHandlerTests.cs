using AutoMapper;

using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Articles.Commands;
using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Tests.Handlers.Articles
{
    public class CreateArticleCommandHandlerTests
    {
        private readonly Mock<IArticleRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task Handle_CreatesArticle_ReturnsArticleDTO()
        {
            // Arrange
            var command = new CreateArticleCommand
            {
                ArticleDTO = new CreateArticleDTO
                {
                    Name = "Test",
                    Description = "Description",
                    AuthorId = "user1"
                }
            };

            var article = new Article { Id = 1, Name = "Test" };
            var articleDto = new ArticleDTO { Id = 1, Name = "Test" };

            _mapperMock.Setup(m => m.Map<Article>(command.ArticleDTO)).Returns(article);
            _mapperMock.Setup(m => m.Map<ArticleDTO>(article)).Returns(articleDto);

            var handler = new CreateArticleCommandHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(article), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.Equal("Test", result.Name);
        }
    }
}
