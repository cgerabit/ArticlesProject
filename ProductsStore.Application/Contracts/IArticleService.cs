using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Contracts
{
    public interface IArticleService
    {
        Task<bool> UserCanManageArticle(int articleId, string userId);
    }
}
