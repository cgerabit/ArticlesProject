using AutoMapper;

using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Infraestructure.Profiles
{
    public class AuthProfiles : Profile
    {

        public AuthProfiles()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(member => member.UserName,options => options.MapFrom(src => src.Email));
            CreateMap<User,UserInfoDTO>()
                .ForMember(m => m.UserId,options => options.MapFrom(src => src.Id));  
        }
    }
}
