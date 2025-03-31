using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Auth.Queries;

namespace ProductsStore.Application.Tests.Handlers.Auth
{
    public class UserInfoQueryHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();

        [Fact]
        public async Task Handle_ReturnsUserInfoDTO_WhenUserExists()
        {
            // Arrange
            string userId = "1";
            UserInfoDTO dto = new() { UserId = userId, UserName= "user@test.com" };

            _ = _authServiceMock.Setup(a => a.GetUserInfo(userId)).ReturnsAsync(dto);

            UserInfoQueryHandler handler = new(_authServiceMock.Object);
            UserInfoQuery query = new() { UserId = userId };

            // Act
            UserInfoDTO result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal("user@test.com", result.UserName);
        }
    }
}
