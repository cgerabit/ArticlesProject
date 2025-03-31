using Moq;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Auth.Commands;

namespace ProductsStore.Application.Tests.Handlers.Auth
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenUserIsRegistered()
        {
            // Arrange
            RegisterDTO dto = new() { Email = "user@test.com", Password = "Test1234" };
            RegisterResponseDTO response = new() { Success = true, Message = "User created successfully" };

            _ = _authServiceMock.Setup(a => a.Register(dto)).ReturnsAsync(response);

            RegisterCommandHandler handler = new(_authServiceMock.Object);
            RegisterCommand command = new() { RegisterDTO = dto };

            // Act
            RegisterResponseDTO result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("User created successfully", result.Message);
        }

        [Fact]
        public async Task Handle_ReturnsError_WhenRegistrationFails()
        {
            // Arrange
            RegisterDTO dto = new() { Email = "invalid@test.com", Password = "123" };
            RegisterResponseDTO response = new() { Success = false, Message = "Invalid password" };

            _ = _authServiceMock.Setup(a => a.Register(dto)).ReturnsAsync(response);

            RegisterCommandHandler handler = new(_authServiceMock.Object);
            RegisterCommand command = new() { RegisterDTO = dto };

            // Act
            RegisterResponseDTO result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid password", result.Message);
        }
    }

}
