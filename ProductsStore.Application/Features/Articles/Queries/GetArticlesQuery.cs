using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Queries
{
    public class GetArticlesQuery : IRequest<IEnumerable<ArticleDTO>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
