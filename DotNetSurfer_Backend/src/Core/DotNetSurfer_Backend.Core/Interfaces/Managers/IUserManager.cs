using DotNetSurfer_Backend.Core.Models;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface IUserManager
    {
        Task SignUp(User model);

        Task<SignIn> SignIn(User model);
    }
}
