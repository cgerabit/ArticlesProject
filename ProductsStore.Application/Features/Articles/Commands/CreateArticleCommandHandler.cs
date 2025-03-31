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

namespace ProductsStore.Application.Features.Articles.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDTO>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public CreateArticleCommandHandler(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArticleDTO> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Article>(request.ArticleDTO);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ArticleDTO>(entity);
        }
    }

}
