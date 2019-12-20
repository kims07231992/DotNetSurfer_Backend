using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.API.Controllers
{
    public class AnnouncementsController : BaseController<AnnouncementsController>
    {
        private readonly IAnnouncementManager _announcementManager;

        public AnnouncementsController(IAnnouncementManager announcementManager, ILogger<AnnouncementsController> logger)
            : base(logger)
        {
            this._announcementManager = announcementManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement([FromRoute] int id)
        {
            Announcement announcement = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                announcement = await this._announcementManager.GetAnnouncement(id);
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

            return Ok(announcement);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            IEnumerable<Announcement> announcements = null;

            try
            {
                announcements = await this._announcementManager.GetAnnouncements();
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

            return Ok(announcements);
        }

        [HttpGet("users/{userId}")]
        [Authorize(Roles = nameof(PermissionType.Admin) + "," + nameof(PermissionType.User))]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncementsByUserId([FromRoute] int userId)
        {
            IEnumerable<Announcement> announcements = null;

            try
            {
                announcements = await this._announcementManager.GetAnnouncementsByUserId(userId, User);
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

            return Ok(announcements);
        }
    }
}