using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Articles.Commands;
using ProductsStore.Application.Features.Articles.Queries;
using ProductsStore.Application.Features.Comments.Commands;

using ProductsStoreApi.Controllers;

using System.Security.Claims;

namespace ProductsStoreApi.Tests.Controllers
{
    public class ArticlesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ArticlesController _controller;

        public ArticlesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ArticlesController(_mediatorMock.Object);
        }
        private void SetUserContext(string userId)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId)
    };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var context = new DefaultHttpContext
            {
                User = principal
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };
        }
        [Fact]
        public async Task GetArticles_ReturnsOkResultWithArticles()
        {
            // Arrange
            List<ArticleDTO> expected = new()
            { new ArticleDTO { Id = 1, Name = "Test" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetArticlesQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetArticles(1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            IEnumerable<ArticleDTO> articles = Assert.IsAssignableFrom<IEnumerable<ArticleDTO>>(okResult.Value);
            Assert.Single(articles);
        }

        [Fact]
        public async Task GetArticleById_ReturnsOkResultWithArticle()
        {
            // Arrange
            var expected = new ArticleDetailsDTO { Id = 1, Name = "Test" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetArticleByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetArticleById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var article = Assert.IsType<ArticleDetailsDTO>(okResult.Value);
            Assert.Equal(1, article.Id);
        }

        [Fact]
        public async Task CreateArticle_ReturnsCreatedAtWithArticle()
        {
            // Arrange
            SetUserContext("1234");

            var dto = new CreateArticleDTO { Name = "New", Description = "Desc", AuthorId = "1" };
            var returnedDto = new ArticleDTO { Id = 1, Name = "New" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateArticleCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(returnedDto);

            // Act
            var result = await _controller.CreateArticle(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var article = Assert.IsType<ArticleDTO>(createdResult.Value);
            Assert.Equal("New", article.Name);
        }

        [Fact]
        public async Task UpdateArticle_ReturnsOkWithUpdatedArticle()
        {
            // Arrange
            SetUserContext("1234");
            var dto = new CreateArticleDTO { Name = "Updated", Description = "Updated", AuthorId = "1" };
            var updated = new ArticleDTO { Id = 1, Name = "Updated" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateArticleCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updated);

            // Act
            var result = await _controller.UpdateArticle(1, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var article = Assert.IsType<ArticleDTO>(okResult.Value);
            Assert.Equal("Updated", article.Name);
        }

        [Fact]
        public async Task DeleteArticle_ReturnsNoContent()
        {
            //arrange
            SetUserContext("1234");

            // Act
            var result = await _controller.DeleteArticle(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task AddComment_ReturnsOkWithComment()
        {
            // Arrange
            SetUserContext("1234");

            var dto = new AddCommentDTO { Text = "Nice!", UserId = "user1" };
            var returned = new CommentDTO { Id = 1, Text = "Nice!" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddCommentCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(returned);

            // Act
            var result = await _controller.AddComment(1, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var comment = Assert.IsType<CommentDTO>(okResult.Value);
            Assert.Equal("Nice!", comment.Text);
        }
    }
}
