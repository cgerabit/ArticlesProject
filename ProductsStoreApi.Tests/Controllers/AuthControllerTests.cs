using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Auth.Commands;
using ProductsStore.Application.Features.Auth.Queries;

using ProductsStoreApi.Controllers;

using System.Security.Claims;

namespace ProductsStoreApi.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsRegisterResponseDTO()
        {
            // Arrange
            RegisterDTO registerDto = new()
            {
                Email = "test@example.com",
                Password = "Test1234"
            };

            RegisterResponseDTO expected = new()
            {
                Success = true,
                Message = "User created successfully"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            ActionResult<RegisterResponseDTO> result = await _controller.Register(registerDto);

            // Assert
            ActionResult<RegisterResponseDTO> actionResult = Assert.IsType<ActionResult<RegisterResponseDTO>>(result);
            RegisterResponseDTO value = Assert.IsType<RegisterResponseDTO>(actionResult.Value);
            Assert.True(value.Success);
            Assert.Equal("User created successfully", value.Message);
        }

        [Fact]
        public async Task Login_ReturnsLoginResponseDTO_Success()
        {
            // Arrange
            LoginDTO loginDto = new()
            {
                Email = "user@example.com",
                Password = "Test1234"
            };

            LoginResponseDTO expected = new()
            {
                IsSuccess = true,
                Response = new JwtResponse
                {
                    Token = "fake-jwt-token"
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            ActionResult<LoginResponseDTO> result = await _controller.Login(loginDto);

            // Assert
            ActionResult<LoginResponseDTO> actionResult = Assert.IsType<ActionResult<LoginResponseDTO>>(result);
            LoginResponseDTO value = Assert.IsType<LoginResponseDTO>(actionResult.Value);
            Assert.True(value.IsSuccess);
            Assert.Equal("fake-jwt-token", value.Response.Token);
        }

        [Fact]
        public async Task Me_ReturnsUserInfoDTO_WhenAuthenticated()
        {
            // Arrange
            string userId = "123";
            UserInfoDTO expected = new()
            {
              UserId = userId,
                UserName = "test",
            };

            // Simular contexto Http con userId
            ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity(new Claim[]
            {
            new(ClaimTypes.NameIdentifier, userId)
            }, JwtBearerDefaults.AuthenticationScheme));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UserInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            ActionResult<UserInfoDTO> result = await _controller.Me();

            // Assert
            ActionResult<UserInfoDTO> actionResult = Assert.IsType<ActionResult<UserInfoDTO>>(result);
            UserInfoDTO value = Assert.IsType<UserInfoDTO>(actionResult.Value);
            Assert.Equal(userId, value.UserId);
        }

        [Fact]
        public async Task Me_ReturnsUnauthorized_WhenUserIdIsMissing()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            // Act
            ActionResult<UserInfoDTO> result = await _controller.Me();

            // Assert
            ActionResult<UserInfoDTO> actionResult = Assert.IsType<ActionResult<UserInfoDTO>>(result);
            _ = Assert.IsType<UnauthorizedResult>(actionResult.Result);
        }
    }
}
