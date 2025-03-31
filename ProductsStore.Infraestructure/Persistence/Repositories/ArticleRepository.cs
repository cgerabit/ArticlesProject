using Microsoft.EntityFrameworkCore;

using ProductsStore.Application.Contracts;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Infraestructure.Persistence.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ProductsStoreDbContext _context;

        public ArticleRepository(ProductsStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Articles
                .Include(a => a.Author)
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Article?> GetByIdWithCommentsAsync(int id)
        {
            return await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Article article)
        {
            _ = await _context.Articles.AddAsync(article);
        }

        public void Update(Article article)
        {
            _ = _context.Articles.Update(article);
        }

        public void Delete(Article article)
        {
            _ = _context.Articles.Remove(article);
        }

        public async Task SaveChangesAsync()
        {
            _ = await _context.SaveChangesAsync();
        }
    }
}
