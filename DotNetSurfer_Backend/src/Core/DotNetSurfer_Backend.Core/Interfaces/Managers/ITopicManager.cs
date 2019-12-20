using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface ITopicManager
    {
        Task<Topic> GetTopic(int id);

        Task<IEnumerable<Topic>> GetTopics();

        Task<IEnumerable<Topic>> GetTopicsByUserId(int userId, ClaimsPrincipal claimsPrincipal);
    }
}
