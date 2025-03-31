using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Queries
{
    public class GetArticleByIdQuery : IRequest<ArticleDetailsDTO>
    {
        public int Id { get; set; }
    }
}
