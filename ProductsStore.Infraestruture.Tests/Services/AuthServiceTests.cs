using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;
using ProductsStore.Infraestructure.Services;

namespace ProductsStore.Infraestruture.Tests.Services
{
    using Xunit;
    using Moq;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authentication;
    using ProductsStore.Domain.Entities;
    using ProductsStore.Application.DTOs;

    public class AuthServiceTests
    {
        private readonly FakeUserManager _userManager;
        private readonly FakeSignInManager _signInManager;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userManager = new FakeUserManager();
            _signInManager = new FakeSignInManager(_userManager);
            _mapperMock = new Mock<IMapper>();

            _authService = new AuthService(_userManager, _signInManager, _mapperMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreCorrect()
        {
            var dto = new LoginDTO { Email = "test@test.com", Password = "Valid123" };
            var user = new User { Id = "1", Email = dto.Email };

            _userManager.OnFindByEmailAsync = email => Task.FromResult<User?>(user);
            _signInManager.OnCheckPasswordSignInAsync = (u, p, l) => Task.FromResult(SignInResult.Success);

            var result = await _authService.Login(dto);

            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenUserNotFound()
        {
            var dto = new LoginDTO { Email = "notfound@test.com", Password = "pass" };
            _userManager.OnFindByEmailAsync = email => Task.FromResult<User?>(null);

            var result = await _authService.Login(dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenPasswordIsWrong()
        {
            var dto = new LoginDTO { Email = "test@test.com", Password = "wrong" };
            var user = new User { Id = "1", Email = dto.Email };

            _userManager.OnFindByEmailAsync = email => Task.FromResult<User?>(user);
            _signInManager.OnCheckPasswordSignInAsync = (u, p, l) => Task.FromResult(SignInResult.Failed);

            var result = await _authService.Login(dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task Register_ReturnsSuccess_WhenUserCreated()
        {
            var dto = new RegisterDTO { Email = "new@test.com", Password = "Valid123" };
            var user = new User { Email = dto.Email };

            _mapperMock.Setup(m => m.Map<User>(dto)).Returns(user);
            _userManager.OnCreateAsync = (u, p) => Task.FromResult(IdentityResult.Success);

            var result = await _authService.Register(dto);

            Assert.True(result.Success);
            Assert.Equal("User created successfully", result.Message);
        }

        [Fact]
        public async Task Register_ReturnsError_WhenCreationFails()
        {
            var dto = new RegisterDTO { Email = "bad@test.com", Password = "123" };
            var user = new User { Email = dto.Email };
            var failed = IdentityResult.Failed(new IdentityError { Description = "Too short" });

            _mapperMock.Setup(m => m.Map<User>(dto)).Returns(user);
            _userManager.OnCreateAsync = (u, p) => Task.FromResult(failed);

            var result = await _authService.Register(dto);

            Assert.False(result.Success);
            Assert.Equal("Too short", result.Message);
        }

        [Fact]
        public async Task GetUserInfo_ReturnsMappedUser()
        {
            var userId = "1";
            var user = new User { Id = userId, Email = "test@test.com" };
            var dto = new UserInfoDTO { UserId = userId, UserName = user.Email };

            _userManager.OnFindByIdAsync = id => Task.FromResult<User?>(user);
            _mapperMock.Setup(m => m.Map<UserInfoDTO>(user)).Returns(dto);

            var result = await _authService.GetUserInfo(userId);

            Assert.Equal(userId, result.UserId);
            Assert.Equal("test@test.com", result.UserName);
        }
    }


}
