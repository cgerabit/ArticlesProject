using MediatR;
using ProductsStore.Application.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Features.Articles.Commands
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly IArticleRepository _repository;
        private readonly IArticleService _articleService;

        public DeleteArticleCommandHandler(IArticleRepository repository,
            IArticleService articleService)
        {
            _repository = repository;
            _articleService = articleService;
        }

        public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var userCanEdit = await _articleService.UserCanManageArticle(request.ArticleId, request.UserId
          );

            if (!userCanEdit)
            {
                throw new UnauthorizedAccessException("No tienes permisos para editar este artículo");
            }

            var article = await _repository.GetByIdWithCommentsAsync(request.ArticleId);
            if (article == null)
                throw new KeyNotFoundException("Artículo no encontrado");

            _repository.Delete(article);
            await _repository.SaveChangesAsync();

        }
    }
}
