using Microsoft.AspNetCore.Identity;

using ProductsStore.Application.Contracts;
using ProductsStore.Domain.Entities;

namespace ProductsStore.Infraestructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly UserManager<User> _userManager;
        private readonly IArticleRepository _articleRepository;

        public ArticleService(UserManager<User> userManager,
            IArticleRepository articleRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
        }

        public async Task<bool> UserCanManageArticle(int articleId, string userId)
        {
            // check if the user is the author of the article
            Article? article = await _articleRepository.GetByIdWithCommentsAsync(articleId);

            if (article == null)
            {
                return false;
            }
            bool isAuthor = article.AuthorId == userId;

            if (isAuthor)
            {
                return true;
            }

            // check if user is in role admin

            User? user = await _userManager.FindByIdAsync(userId);

            return user != null && await _userManager.IsInRoleAsync(user, "Administrator");
        }
    }
}
