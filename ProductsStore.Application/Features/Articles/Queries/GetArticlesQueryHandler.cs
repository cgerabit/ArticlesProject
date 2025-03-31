using AutoMapper;

using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Queries
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, IEnumerable<ArticleDTO>>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public GetArticlesQueryHandler(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleDTO>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Article> articles = await _repository.GetAllAsync(request.Page, request.PageSize);
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }
    }
}
