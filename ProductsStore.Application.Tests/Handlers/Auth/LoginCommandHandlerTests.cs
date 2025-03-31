using Moq;
using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Auth.Commands;
using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Tests.Handlers.Auth
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IJwtProvider> _jwtProviderMock = new();

        [Fact]
        public async Task Handle_ReturnsToken_WhenLoginIsSuccessful()
        {
            // Arrange
            var dto = new LoginDTO { Email = "user@test.com", Password = "Test1234" };
            var user = new User { Id = "1", Email = "user@test.com" };
            var jwt = new JwtResponse { Token = "jwt-token" };

            _authServiceMock.Setup(a => a.Login(dto)).ReturnsAsync(user);
            _jwtProviderMock.Setup(j => j.GetUserToken(user, null)).Returns(jwt);

            var handler = new LoginCommandHandler(_authServiceMock.Object, _jwtProviderMock.Object);
            var command = new LoginCommand { LoginDTO = dto };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("jwt-token", result.Response.Token);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenLoginFails()
        {
            // Arrange
            var dto = new LoginDTO { Email = "invalid@test.com", Password = "wrong" };

            _authServiceMock.Setup(a => a.Login(dto)).ReturnsAsync((User?)null);

            var handler = new LoginCommandHandler(_authServiceMock.Object, _jwtProviderMock.Object);
            var command = new LoginCommand { LoginDTO = dto };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Response);
        }
    }

}
