using MediatR;

namespace ProductsStore.Application.Features.Articles.Commands
{
    public class DeleteArticleCommand : IRequest
    {
        public int ArticleId { get; set; }

        public string UserId { get; set; }
    }
}
