using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Contracts
{
    public interface IAuthService
    {
        Task<RegisterResponseDTO> Register(RegisterDTO registerDTO);
        Task<User?> Login(LoginDTO loginDTO);
        Task<UserInfoDTO?> GetUserInfo(string userId);
    }
}
