using AutoMapper;

using MediatR;

using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Features.Comments.Commands
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommentDTO>
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public AddCommentCommandHandler(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommentDTO> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request.CommentDTO);
            comment.ArticleId = request.ArticleId;
            comment.PublishedAt = DateTime.UtcNow;

            await _repository.AddAsync(comment);
            await _repository.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }
    }
}
