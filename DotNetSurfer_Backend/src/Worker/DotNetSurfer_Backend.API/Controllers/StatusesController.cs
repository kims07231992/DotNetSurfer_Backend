using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.API.Controllers
{
    public class StatusesController : BaseController<StatusesController>
    {
        private readonly IStatusManager _statusManager;

        public StatusesController(IStatusManager statusManager, ILogger<StatusesController> logger)
            : base(logger)
        {
            this._statusManager = statusManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> Statuses()
        {
            IEnumerable<Status> statuses = null;

            try
            {
                statuses = await this._statusManager.GetStatuses();
            }
            catch (CustomUnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (CustomNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BaseCustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return BadRequest(new BaseCustomException().Message);
            }

            return Ok(statuses);
        }
    }
}