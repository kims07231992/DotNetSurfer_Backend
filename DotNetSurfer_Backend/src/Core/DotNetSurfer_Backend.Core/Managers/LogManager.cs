using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class LogManager : BaseManager<LogManager>, ILogManager
    {
        public LogManager(IUnitOfWork unitOfWork, ILogger<LogManager> logger)
           : base(unitOfWork, logger)
        {
        }

        public Task WriteErrorLog(string message)
        {
            try
            {
                this._logger.LogError(message);
            }
            catch (Exception ex)
            {
                throw new BaseCustomException();
            }

            return Task.CompletedTask;
        }

        public Task WriteInfoLog(string message)
        {
            try
            {
                this._logger.LogInformation(message);
            }
            catch (Exception ex)
            {
                throw new BaseCustomException();
            }

            return Task.CompletedTask;
        }
    }
}
