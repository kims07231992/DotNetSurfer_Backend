using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<bool> IsArticleExistAsync(int id);
        Task<Article> GetArticleAsync(int id);
        Task<Article> GetArticleDetailAsync(int id);
        Task<IEnumerable<Article>> GetArticlesByUserIdAsync(int contentLength);
        Task<IEnumerable<Article>> GetArticlesByUserIdAsync(int userId, int contentLength);
        Task<IEnumerable<Article>> GetArticlesByPageAsync(int pageId, int itemPerPage);
        Task<IEnumerable<Article>> GetTopArticlesAsync(int item, int contentLength);
        Task IncreaseArticleReadCountAsync(int id);
    }
}
