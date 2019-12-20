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
    public class HeadersController : BaseController<HeadersController>
    {
        private readonly IHeaderManager _headerManager;

        public HeadersController(IHeaderManager headerManager, ILogger<HeadersController> logger)
            : base(logger)
        {
            this._headerManager = headerManager;
        }

        [HttpGet("menu/side")]
        public async Task<ActionResult<IEnumerable<Header>>> GetSideHeaderMenus()
        {
            IEnumerable<Header> sideMenus = null;

            try
            {
                sideMenus = await this._headerManager.GetSideHeaderMenus();
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

            return Ok(sideMenus);
        }
    }
}