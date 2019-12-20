using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Extensions;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class HeaderManager : BaseManager<HeaderManager>, IHeaderManager
    {
        public HeaderManager(IUnitOfWork unitOfWork, ILogger<HeaderManager> logger)
           : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Header>> GetSideHeaderMenus()
        {
            IEnumerable<Header> sideMenus = null;

            try
            {
                var topics = await this._unitOfWork.TopicRepository.GetSideHeaderMenusAsync();
                sideMenus = topics?.Select(h => h.MapToDomainHeader());
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

            return sideMenus;
        }
    }
}
