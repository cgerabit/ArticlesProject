using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProductsStore.Application.DTOs;
using ProductsStore.Application.Features.Articles.Commands;
using ProductsStore.Application.Features.Articles.Queries;
using ProductsStore.Application.Features.Comments.Commands;

using ProductsStoreApi.ExtensionMethods;

namespace ProductsStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/articles?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            GetArticlesQuery query = new() { Page = page, PageSize = pageSize };
            IEnumerable<ArticleDTO> result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDetailsDTO>> GetArticleById(int id)
        {
            GetArticleByIdQuery query = new() { Id = id };
            ArticleDetailsDTO result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST: api/articles
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ArticleDTO>> CreateArticle([FromBody] CreateArticleDTO dto)
        {
            string? userId = HttpContext.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            dto.AuthorId = userId;

            CreateArticleCommand command = new() { ArticleDTO = dto };
            ArticleDTO result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetArticleById), new { id = result.Id }, result);
        }

        // PUT: api/articles/{id}
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<ArticleDTO>> UpdateArticle(int id, [FromBody] CreateArticleDTO dto)
        {
            string? userId = HttpContext.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            dto.AuthorId = userId;
            UpdateArticleCommand command = new()
            {
                ArticleId = id,
                ArticleDTO = dto
            };



            ArticleDTO result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/articles/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult> DeleteArticle(int id)
        {
            string? userId = HttpContext.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            DeleteArticleCommand command = new()
            {
                ArticleId = id,

                UserId = userId
            };
            await _mediator.Send(command);
            return NoContent();
        }

        // POST: api/articles/{id}/comments
        [HttpPost("{id}/comments")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<CommentDTO>> AddComment(int id, [FromBody] AddCommentDTO dto)
        {

            string? userId = HttpContext.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            dto.UserId = userId;
            AddCommentCommand command = new()
            {
                ArticleId = id,
                CommentDTO = dto,

            };

            CommentDTO result = await _mediator.Send(command);
            return Ok(result);
        }
    }

}
