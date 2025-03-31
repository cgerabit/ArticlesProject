using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Commands
{
    public class CreateArticleCommand : IRequest<ArticleDTO>
    {
        public required CreateArticleDTO ArticleDTO { get; set; }
    }
}
