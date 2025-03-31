using MediatR;

using ProductsStore.Application.DTOs;

namespace ProductsStore.Application.Features.Comments.Commands
{
    public class AddCommentCommand : IRequest<CommentDTO>
    {
        public int ArticleId { get; set; }
        public required AddCommentDTO CommentDTO { get; set; }
    }
}
