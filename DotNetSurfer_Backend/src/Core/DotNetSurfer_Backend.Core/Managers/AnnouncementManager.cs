using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Caches;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class AnnouncementManager : BaseManager<AnnouncementManager>, IAnnouncementManager
    {
        public AnnouncementManager(
            IUnitOfWork unitOfWork,
            ICacheDataProvider cacheDataProvider, 
            ILogger<AnnouncementManager> logger
            ) : base(unitOfWork, cacheDataProvider, logger)
        {
        }

        public async Task<Announcement> GetAnnouncement(int id)
        {
            Announcement announcement = null;

            try
            {
                announcement = await this._unitOfWork.AnnouncementRepository.GetAnnouncementAsync(id);
                if (announcement == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, id.ToString());
                throw new BaseCustomException();
            }

            return announcement;
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncements()
        {
            IEnumerable<Announcement> announcements = null;

            try
            {
                announcements = await this._unitOfWork.AnnouncementRepository.GetAnnouncementsAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                throw new BaseCustomException();
            }

            return announcements;
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByUserId(int userId, ClaimsPrincipal claimsPrincipal)
        {
            IEnumerable<Announcement> announcements = null;

            try
            {
                bool isUserExist = await this._unitOfWork.UserRepository.IsUserExistAsync(userId);
                if (!isUserExist)
                {
                    throw new CustomNotFoundException($"UserID: {userId}");
                }

                announcements = IsAdministrator(claimsPrincipal)
                    ? await this._unitOfWork.AnnouncementRepository.GetAnnouncementsByUserIdAsync()
                    : await this._unitOfWork.AnnouncementRepository.GetAnnouncementsByUserIdAsync(userId);
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, userId.ToString());
                throw new BaseCustomException();
            }

            return announcements;
        }
    }
}
