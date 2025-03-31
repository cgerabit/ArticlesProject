using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Commands
{
    public class UpdateArticleCommand : IRequest<ArticleDTO>
    {
        public int ArticleId { get; set; }
        public required CreateArticleDTO ArticleDTO { get; set; }
    }
}
