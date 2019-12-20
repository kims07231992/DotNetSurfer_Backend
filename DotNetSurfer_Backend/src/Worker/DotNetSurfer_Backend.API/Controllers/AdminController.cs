using System;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetSurfer_Backend.API.Controllers
{
    [Authorize(Roles = nameof(PermissionType.Admin) + "," + nameof(PermissionType.User))]
    public class AdminController : BaseController<AdminController>
    {
        private readonly IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager, ILogger<AdminController> logger)
            : base(logger)
        {
            this._adminManager = adminManager;
        }

        #region Topics
        [HttpPut("topics/{id}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] int id, [FromBody] Topic topic)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.UpdateTopic(id, topic, User);
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

            return Ok(topic);
        }

        [HttpPost("topics")]
        public async Task<IActionResult> CreateTopic([FromBody] Topic topic)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.CreateTopic(topic);
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

            return CreatedAtAction(nameof(Topic), new { id = topic.TopicId }, topic);
        }

        [HttpDelete("topics/{id}")]
        public async Task<IActionResult> DeleteTopic([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.DeleteTopic(id, User);
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

            return Ok();
        }
        #endregion

        #region Articles
        [HttpPut("articles/{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] int id, [FromBody] Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.UpdateArticle(id, article, User);
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

            return Ok(article);
        }

        [HttpPost("articles")]
        public async Task<IActionResult> CreateArticle([FromBody] Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.CreateArticle(article);
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

            return CreatedAtAction(nameof(Article), new { id = article.ArticleId }, article);
        }

        [HttpDelete("articles/{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.DeleteArticle(id, User);
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

            return Ok();
        }
        #endregion

        #region Announcements
        [HttpPut("announcements/{id}")]
        public async Task<IActionResult> UpdateAnnouncement([FromRoute] int id, [FromBody] Announcement announcement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.UpdateAnnouncement(id, announcement, User);
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

        [HttpPost("announcements")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] Announcement announcement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.CreateAnnouncement(announcement);
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

            return CreatedAtAction(nameof(Announcement), new { id = announcement.AnnouncementId }, announcement);
        }

        [HttpDelete("announcements/{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.DeleteAnnouncement(id, User);
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

            return Ok();
        }
        #endregion

        #region Users
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._adminManager.UpdateUser(id, user, User);
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

            return Ok(user);
        }
        #endregion
    }
}