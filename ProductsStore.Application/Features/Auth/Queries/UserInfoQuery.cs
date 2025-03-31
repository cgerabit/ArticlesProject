using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Auth.Queries
{
    public class UserInfoQuery : IRequest<UserInfoDTO>
    {
        public required string UserId { get; set; }
    }
}
