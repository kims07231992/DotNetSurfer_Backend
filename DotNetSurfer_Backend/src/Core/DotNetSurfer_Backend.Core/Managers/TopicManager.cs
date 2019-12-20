using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class TopicManager : BaseManager<TopicManager>, ITopicManager
    {
        public TopicManager(IUnitOfWork unitOfWork, ILogger<TopicManager> logger)
           : base(unitOfWork, logger)
        {
        }

        public async Task<Topic> GetTopic(int id)
        {
            Topic topic = null;

            try
            {
                topic = await this._unitOfWork.TopicRepository
                    .GetTopicAsync(id);

                if (topic == null)
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

            return topic;
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            IEnumerable<Topic> topics = null;

            try
            {
                topics = await this._unitOfWork.TopicRepository.GetTopicsAsync();
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

            return topics;
        }

        public async Task<IEnumerable<Topic>> GetTopicsByUserId(int userId, ClaimsPrincipal claimsPrincipal)
        {
            IEnumerable<Topic> topics = null;

            try
            {
                bool isUserExist = await this._unitOfWork.UserRepository.IsUserExistAsync(userId);
                if (!isUserExist)
                {
                    throw new CustomNotFoundException($"UserID: {userId}");
                }

                topics = IsAdministrator(claimsPrincipal)
                    ? await this._unitOfWork.TopicRepository.GetTopicsByUserIdAsync()
                    : await this._unitOfWork.TopicRepository.GetTopicsByUserIdAsync(userId);
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

            return topics;
        }
    }
}
