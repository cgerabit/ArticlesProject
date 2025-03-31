using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Contracts
{
    public interface IJwtProvider
    {
        JwtResponse GetUserToken(User user, string? role);
    }
}
