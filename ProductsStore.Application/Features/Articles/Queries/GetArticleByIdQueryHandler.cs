using AutoMapper;

using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Queries
{
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDetailsDTO>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public GetArticleByIdQueryHandler(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArticleDetailsDTO> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Article? article = await _repository.GetByIdWithCommentsAsync(request.Id);
            return article == null ? throw new KeyNotFoundException("Artículo no encontrado") : _mapper.Map<ArticleDetailsDTO>(article);
        }
    }
}
