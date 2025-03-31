using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Auth.Commands;
using ProductsStore.Application.Features.Auth.Queries;

using ProductsStoreApi.ExtensionMethods;

namespace ProductsStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDTO>> Register(RegisterDTO registerDTO)
        {
            RegisterResponseDTO result = await _mediator.Send(new RegisterCommand() { RegisterDTO = registerDTO });

            return result;
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {

            var result = await _mediator.Send(new LoginCommand() { LoginDTO = loginDTO });


            return result;
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserInfoDTO>> Me()
        {

            var userId = HttpContext.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(new UserInfoQuery() { UserId = userId});


            return result;
        }
    }
}
