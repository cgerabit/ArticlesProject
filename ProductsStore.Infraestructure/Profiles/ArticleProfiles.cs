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
    public class ArticleProfiles : Profile
    {
        public ArticleProfiles()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Article, ArticleDetailsDTO>().ReverseMap();
            CreateMap<CreateArticleDTO, Article>();

            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<AddCommentDTO, Comment>();
            CreateMap<User, UserDTO>();
        }
    }
}
