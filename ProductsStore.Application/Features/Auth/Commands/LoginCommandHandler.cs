using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Features.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IAuthService _authService;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IAuthService authService,
            IJwtProvider jwtProvider)
        {
            _authService = authService;
            _jwtProvider = jwtProvider;
        }
        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _authService.Login(request.LoginDTO);

            if (user == null)
            {
                return new LoginResponseDTO
                {
                    IsSuccess = false,
                };
            }

            JwtResponse token = _jwtProvider.GetUserToken(user, null);

            return new LoginResponseDTO
            {
                IsSuccess = true,
                Response = token
            };
        }
    }
}
