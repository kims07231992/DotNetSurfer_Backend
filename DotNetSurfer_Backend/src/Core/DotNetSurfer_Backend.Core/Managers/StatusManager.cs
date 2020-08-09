using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Caches;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class StatusManager : BaseManager<StatusManager>, IStatusManager
    {
        public StatusManager(
            IUnitOfWork unitOfWork,
            ICacheDataProvider cacheDataProvider,
            ILogger<StatusManager> logger
            ) : base(unitOfWork, cacheDataProvider, logger)
        {
        }

        public async Task<IEnumerable<Status>> GetStatuses()
        {
            IEnumerable<Status> statuses = null;

            try
            {
                statuses = await _cacheDataProvider.TryGetStatusesAsync();
                if (statuses == null)
                {
                    var entityModels = await this._unitOfWork.StatusRepository.GetStatusesAsync();
                    statuses = entityModels?.Select(s => s);

                    await _cacheDataProvider.SetStatusesAsync(statuses);
                }
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

            return statuses;
        }
    }
}
