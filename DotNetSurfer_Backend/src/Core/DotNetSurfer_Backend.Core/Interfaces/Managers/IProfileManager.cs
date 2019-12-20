using DotNetSurfer_Backend.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface IProfileManager
    {
        Task<User> GetUser(int id, ClaimsPrincipal claimsPrincipal);
    }
}
