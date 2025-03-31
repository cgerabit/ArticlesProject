using MediatR;

using ProductsStore.Application.DTOs;



namespace ProductsStore.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<RegisterResponseDTO>
    {
        public required RegisterDTO RegisterDTO { get; set; }

    }


}

