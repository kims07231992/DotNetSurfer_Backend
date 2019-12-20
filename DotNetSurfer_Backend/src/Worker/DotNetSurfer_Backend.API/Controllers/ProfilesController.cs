using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.API.Controllers
{
    public class ProfilesController : BaseController<ProfilesController>
    {
        private readonly IProfileManager _profileManager;

        public ProfilesController(IProfileManager profileManager, ILogger<ProfilesController> logger)
            : base(logger)
        {
            this._profileManager = profileManager;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(PermissionType.Admin) + "," + nameof(PermissionType.User))]
        public async Task<ActionResult<User>> GetUser([FromRoute] int id)
        {
            User user = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                user = await this._profileManager.GetUser(id, User);
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
    }
}