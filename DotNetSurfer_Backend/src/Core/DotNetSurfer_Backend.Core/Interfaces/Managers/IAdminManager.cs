using DotNetSurfer_Backend.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface IAdminManager
    {
        #region Topics
        Task UpdateTopic(int id, Topic topic, ClaimsPrincipal claimsPrincipal);

        Task CreateTopic(Topic topic);

        Task DeleteTopic(int id, ClaimsPrincipal claimsPrincipal);
        #endregion

        #region Articles
        Task UpdateArticle(int id, Article article, ClaimsPrincipal claimsPrincipal);

        Task CreateArticle(Article article);

        Task DeleteArticle(int id, ClaimsPrincipal claimsPrincipal);
        #endregion

        #region Announcements
        Task UpdateAnnouncement(int id, Announcement announcement, ClaimsPrincipal claimsPrincipal);

        Task CreateAnnouncement(Announcement announcement);

        Task DeleteAnnouncement(int id, ClaimsPrincipal claimsPrincipal);
        #endregion

        #region Users
        Task UpdateUser(int id, User user, ClaimsPrincipal claimsPrincipal);
        #endregion
    }
}
