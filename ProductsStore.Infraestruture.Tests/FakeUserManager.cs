using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using ProductsStore.Domain.Entities;

namespace ProductsStore.Infraestruture.Tests
{
    public class FakeUserManager : UserManager<User>
    {
        public Func<string, Task<User?>>? OnFindByEmailAsync;
        public Func<User, string, Task<IdentityResult>>? OnCreateAsync;
        public Func<string, Task<User?>>? OnFindByIdAsync;
        public Func<User, IList<string>>? OnGetRolesAsync;

        public FakeUserManager()
            : base(
                new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new PasswordHasher<User>(),
                Array.Empty<IUserValidator<User>>(),
                Array.Empty<IPasswordValidator<User>>(),
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object)
        {
        }

        public override Task<User?> FindByEmailAsync(string email)
        {
            return OnFindByEmailAsync?.Invoke(email) ?? Task.FromResult<User?>(null);
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            return OnCreateAsync?.Invoke(user, password) ?? Task.FromResult(IdentityResult.Failed());
        }

        public override Task<User?> FindByIdAsync(string userId)
        {
            return OnFindByIdAsync?.Invoke(userId) ?? Task.FromResult<User?>(null);
        }

        public override Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.FromResult(OnGetRolesAsync?.Invoke(user) ?? new List<string>());
        }
    }
}
