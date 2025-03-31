using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDTO>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler( IAuthService authService)
        {
            this._authService = authService;
        }
        public Task<RegisterResponseDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return _authService.Register(request.RegisterDTO);
        }
    }
}
