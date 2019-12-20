using System.Threading.Tasks;
using DotNetSurfer_Backend.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer.API.Web.Controllers
{
    public class LogsController : BaseController<LogsController>
    {
        private readonly ILogManager _logManager;

        public LogsController(ILogManager logManager, ILogger<LogsController> logger)
            : base(logger)
        {
            this._logManager = logManager;
        }

        [HttpPost("error")]
        public async Task<ActionResult> WriteErrorLog(string message)
        {
            try
            {
                await this._logManager.WriteErrorLog(message);
            }
            catch (BaseCustomException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("info")]
        public async Task<ActionResult> WriteInfoLog(string message)
        {
            try
            {
                await this._logManager.WriteInfoLog(message);
            }
            catch (BaseCustomException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}