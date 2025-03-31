using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Features.Auth.Queries
{
    public class UserInfoQueryHandler : IRequestHandler<UserInfoQuery, UserInfoDTO>
    {
        private readonly IAuthService _authService;

        public UserInfoQueryHandler( IAuthService authService)
        {
            this._authService = authService;
        }
        public async Task<UserInfoDTO> Handle(UserInfoQuery request, CancellationToken cancellationToken)
        {

            return await _authService.GetUserInfo(request.UserId);
        }
    }
}
