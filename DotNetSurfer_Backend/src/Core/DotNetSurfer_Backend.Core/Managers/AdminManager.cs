using DotNetSurfer_Backend.Core.Interfaces.Managers;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Interfaces.Encryptors;
using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Extensions;
using DotNetSurfer_Backend.Core.Interfaces.Caches;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class AdminManager : BaseManager<AdminManager>, IAdminManager
    {
        private readonly IEncryptor _encryptor;

        public AdminManager(
            IEncryptor encryptor,
            IUnitOfWork unitOfWork,
            ICacheDataProvider cacheDataProvider,
            ILogger<AdminManager> logger
            ) : base(unitOfWork, cacheDataProvider, logger)
        {
            this._encryptor = encryptor;
        }

        #region Topics
        public async Task UpdateTopic(int id, Topic topic, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                if (id != topic.TopicId)
                {
                    throw new CustomArgumentException($"ID: {id}, TopicID: {topic.TopicId}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && topic.UserId != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }

                this._unitOfWork.TopicRepository.Update(topic);
                await this._unitOfWork.TopicRepository.SaveAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, topic.ToJson());
                throw new BaseCustomException();
            }
        }

        public async Task CreateTopic(Topic topic)
        {
            try
            {
                bool isTitleExist = await this._unitOfWork.TopicRepository.IsTitleExistAsync(topic.Title);
                if (isTitleExist)
                {
                    throw new BaseCustomException("Title already exists");
                }

                this._unitOfWork.TopicRepository.Create(topic);
                await this._unitOfWork.TopicRepository.SaveAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, topic.ToJson());
                throw new BaseCustomException();
            }
        }

        public async Task DeleteTopic(int id, ClaimsPrincipal claimsPrincipal)
        {
            Topic entityModel = null;

            try
            {
                entityModel = await this._unitOfWork.TopicRepository.GetTopicAsync(id);
                if (entityModel == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && entityModel.UserId != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }

                this._unitOfWork.TopicRepository.Delete(entityModel);
                await this._unitOfWork.TopicRepository.SaveAsync();
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
        }
        #endregion

        #region Articles
        public async Task UpdateArticle(int id, Article article, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                if (id != article.ArticleId)
                {
                    throw new CustomArgumentException($"ID: {id}, ArticleID: {article.TopicId}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && article.UserId != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }

                this._unitOfWork.ArticleRepository.Update(article);
                await this._unitOfWork.ArticleRepository.SaveAsync();

                // Clear cache to apply update
                await this._cacheDataProvider.ClearArticlesByPageAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, article.ToJson());
                throw new BaseCustomException();
            }
        }

        public async Task CreateArticle(Article article)
        {
            try
            {
                this._unitOfWork.ArticleRepository.Create(article);
                await this._unitOfWork.ArticleRepository.SaveAsync();

                // Clear cache to apply update
                await this._cacheDataProvider.ClearArticlesByPageAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, article.ToJson());
                throw new BaseCustomException();
            }
        }

        public async Task DeleteArticle(int id, ClaimsPrincipal claimsPrincipal)
        {
            Article entityModel = null;

            try
            {
                entityModel = await this._unitOfWork.ArticleRepository.GetArticleAsync(id);
                if (entityModel == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && entityModel.UserId != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }

                this._unitOfWork.ArticleRepository.Delete(entityModel);
                await this._unitOfWork.ArticleRepository.SaveAsync();

                // Clear cache to apply update
                await this._cacheDataProvider.ClearArticlesByPageAsync();
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
        }
        #endregion

        #region Announcements
        public async Task UpdateAnnouncement(int id, Announcement announcement, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                if (id != announcement.AnnouncementId)
                {
                    throw new CustomArgumentException($"ID: {id}, AnnouncementID: {announcement.AnnouncementId}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && announcement.UserId != userId.Value)
                {
                    throw new CustomArgumentException();
                }

                this._unitOfWork.AnnouncementRepository.Update(announcement);
                await this._unitOfWork.AnnouncementRepository.SaveAsync();
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
        }

        public async Task CreateAnnouncement(Announcement announcement)
        {
            try
            {
                this._unitOfWork.AnnouncementRepository.Create(announcement);
                await this._unitOfWork.AnnouncementRepository.SaveAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, announcement.ToJson());
                throw new BaseCustomException();
            }
        }

        public async Task DeleteAnnouncement(int id, ClaimsPrincipal claimsPrincipal)
        {
            Announcement entityModel = null;

            try
            {
                entityModel = await this._unitOfWork.AnnouncementRepository.GetAnnouncementAsync(id);
                if (entityModel == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && entityModel.UserId != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }

                this._unitOfWork.AnnouncementRepository.Delete(entityModel);
                await this._unitOfWork.AnnouncementRepository.SaveAsync();
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
        }
        #endregion

        #region Users
        public async Task UpdateUser(int id, User user, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                if (id != user.UserId)
                {
                    throw new CustomArgumentException($"ID: {id}, UserID: {user.UserId}");
                }

                // Author check
                int? userId = GetUserIdFromClaims(claimsPrincipal);
                if (!IsAdministrator(claimsPrincipal) && user.UserId != userId.Value)
                {
                    throw new CustomUnauthorizedException();
                }

                var currentUser = await this._unitOfWork.UserRepository.GetUserAsNoTrackingAsync(id); // To avoid context tracking exception
                if (!this._encryptor.IsEqual(user.Password, currentUser.Password))
                {
                    throw new CustomUnauthorizedException("Password confirmation does not match.");
                }

                // Email check, if there is already an email trying to change
                bool isUserEmailExist = await this._unitOfWork.UserRepository.IsEmailExistAsync(user.Email);
                bool isAlreadyEmailExists = currentUser.Email != user.Email
                    && isUserEmailExist;
                if (isAlreadyEmailExists)
                {
                    throw new BaseCustomException("User email already exists");
                }

                user.Password = _encryptor.Encrypt(user.Password);

                this._unitOfWork.UserRepository.Update(user);
                await this._unitOfWork.UserRepository.SaveAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, user.ToJson());
                throw new BaseCustomException();
            }
        }
        #endregion
    }
}
