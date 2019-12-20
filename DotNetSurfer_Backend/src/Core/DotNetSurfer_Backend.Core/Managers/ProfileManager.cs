using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class ProfileManager : BaseManager<ProfileManager>, IProfileManager
    {
        public ProfileManager(IUnitOfWork unitOfWork, ILogger<ProfileManager> logger)
           : base(unitOfWork, logger)
        {
        }

        public async Task<User> GetUser(int id, ClaimsPrincipal claimsPrincipal)
        {
            User user = null;

            try
            {
                user = await this._unitOfWork.UserRepository.GetUserAsync(id);
                if (user == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && id != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(GetUser));
            }

            return user;
        }
    }
}
