using DotNetSurfer_Backend.Core.Models;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsUserExistAsync(int id);
        Task<bool> IsEmailExistAsync(string email);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserAsNoTrackingAsync(int id);
    }
}
