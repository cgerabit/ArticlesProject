using ProductsStore.Domain.Entities;

namespace ProductsStore.Application.Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync(int page, int pageSize);
        Task<Article?> GetByIdWithCommentsAsync(int id);
        Task AddAsync(Article article);
        void Update(Article article);
        void Delete(Article article);
        Task SaveChangesAsync();
    }
}
