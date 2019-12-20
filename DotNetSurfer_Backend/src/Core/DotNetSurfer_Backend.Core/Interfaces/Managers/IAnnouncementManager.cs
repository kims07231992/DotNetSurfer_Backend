using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface IAnnouncementManager
    {
        Task<Announcement> GetAnnouncement(int id);

        Task<IEnumerable<Announcement>> GetAnnouncements();

        Task<IEnumerable<Announcement>> GetAnnouncementsByUserId(int userId, ClaimsPrincipal claimsPrincipal);
    }
}
