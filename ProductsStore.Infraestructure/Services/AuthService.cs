using AutoMapper;

using Microsoft.AspNetCore.Identity;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Infraestructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }
        
        public async Task<UserInfoDTO?> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var dto =  _mapper.Map<UserInfoDTO>(user);

            var roles = await _userManager.GetRolesAsync(user);

            dto.Roles = roles != null ? [.. roles] : new List<string>();
            return dto;
        }
        public async Task<User?> Login(LoginDTO loginDTO)
        {

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if(user==null)
            {
                return null;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,true);

            if (result.Succeeded)
            {
                return user;
            }

            return null;

        }

        public async Task<RegisterResponseDTO> Register(RegisterDTO registerDTO)
        {
            var user =_mapper.Map<User>(registerDTO);

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                return new RegisterResponseDTO
                {
                    Success = true,
                    Message = "User created successfully"
                };
            }

            return new RegisterResponseDTO
            {
                Success = false,
                Message = result.Errors.Select(e => e.Description).FirstOrDefault() ?? "An error occurred"
            };

        }
    }
}
