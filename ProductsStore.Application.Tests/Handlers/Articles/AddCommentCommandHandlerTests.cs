using AutoMapper;

using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Comments.Commands;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Tests.Handlers.Articles
{
    public class AddCommentCommandHandlerTests
    {
        private readonly Mock<ICommentRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task Handle_AddsComment_ReturnsCommentDTO()
        {
            // Arrange
            AddCommentCommand command = new()
            {
                ArticleId = 1,
                CommentDTO = new AddCommentDTO { Text = "Nice", UserId = "user1" }
            };

            Comment comment = new() { Id = 1, Text = "Nice", UserId = "user1", ArticleId = 1 };
            CommentDTO dto = new() { Id = 1, Text = "Nice" };

            _ = _mapperMock.Setup(m => m.Map<Comment>(command.CommentDTO)).Returns(comment);
            _ = _mapperMock.Setup(m => m.Map<CommentDTO>(comment)).Returns(dto);

            AddCommentCommandHandler handler = new(_repositoryMock.Object, _mapperMock.Object);

            // Act
            CommentDTO result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(comment), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.Equal("Nice", result.Text);
        }
    }

}
