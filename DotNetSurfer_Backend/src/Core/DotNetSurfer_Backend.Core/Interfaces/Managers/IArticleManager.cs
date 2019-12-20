using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface IArticleManager
    {
        Task<Article> GetArticle(int id);

        Task<Article> GetArticleDetail(int id);

        Task<IEnumerable<Article>> GetArticlesByUserIdAsync(int userId, int tableContentLength, ClaimsPrincipal claimsPrincipal);

        Task<IEnumerable<Article>> GetArticlesByPage(int pageId, int itemPerPage);

        Task<IEnumerable<Article>> GetTopArticles(int itemPerPage, int cardContentLength);
    }
}
