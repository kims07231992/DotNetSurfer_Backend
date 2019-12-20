using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Repositories
{
    public interface ITopicRepository : IRepository<Topic>
    {
        Task<bool> IsTopicExistAsync(int id);
        Task<bool> IsTitleExistAsync(string title);
        Task<Topic> GetTopicAsync(int id);
        Task<IEnumerable<Topic>> GetTopicsAsync();
        Task<IEnumerable<Topic>> GetTopicsByUserIdAsync();
        Task<IEnumerable<Topic>> GetTopicsByUserIdAsync(int userId);
        Task<IEnumerable<Topic>> GetSideHeaderMenusAsync();
    }
}
