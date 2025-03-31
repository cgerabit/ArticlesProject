using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using ProductsStore.Domain.Entities;

namespace ProductsStore.Infraestruture.Tests
{
    public class FakeSignInManager : SignInManager<User>
    {
        public Func<User, string, bool, Task<SignInResult>>? OnCheckPasswordSignInAsync;

        public FakeSignInManager(UserManager<User> userManager)
            : base(
                userManager,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object
            )
        {
        }

        public override Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return OnCheckPasswordSignInAsync?.Invoke(user, password, lockoutOnFailure)
                   ?? Task.FromResult(SignInResult.Failed);
        }
    }
}
