using ProductsStore.Application.Contracts;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Infraestructure.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ProductsStoreDbContext _context;

        public CommentRepository(ProductsStoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            _ = await _context.Comments.AddAsync(comment);
        }

        public async Task SaveChangesAsync()
        {
            _ = await _context.SaveChangesAsync();
        }
    }
}
