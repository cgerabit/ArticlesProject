using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponseDTO>
    {
        public required LoginDTO LoginDTO { get; set; }
    }
}
