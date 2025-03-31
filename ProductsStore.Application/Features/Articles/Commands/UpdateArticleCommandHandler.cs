using AutoMapper;

using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Articles.Commands
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleDTO>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;

        public UpdateArticleCommandHandler(IArticleRepository repository, 
            IMapper mapper,
            IArticleService articleService)
        {
            _repository = repository;
            _mapper = mapper;
            _articleService = articleService;
        }

        public async Task<ArticleDTO> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {

            var userCanEdit = await _articleService.UserCanManageArticle(request.ArticleId,request.ArticleDTO.AuthorId 
                );

            if (!userCanEdit)
            {
                throw new UnauthorizedAccessException("No tienes permisos para editar este artículo");
            }

            Domain.Entities.Article? article = await _repository.GetByIdWithCommentsAsync(request.ArticleId);
            if (article == null)
            {
                throw new KeyNotFoundException("Artículo no encontrado");
            }

            _ = _mapper.Map(request.ArticleDTO, article); // mapea cambios sobre la instancia

            _repository.Update(article);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ArticleDTO>(article);
        }
    }
}
