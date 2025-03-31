using Microsoft.EntityFrameworkCore;

using ProductsStore.Application.Contracts;

namespace ProductsStore.Infraestructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProductsStoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ProductsStoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            _ = await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _ = _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _ = _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            _ = await _context.SaveChangesAsync();
        }
    }
}
