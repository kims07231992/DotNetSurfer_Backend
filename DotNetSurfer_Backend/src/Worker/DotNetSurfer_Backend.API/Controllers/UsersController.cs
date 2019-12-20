using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.API.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersController : BaseController<UsersController>
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager, ILogger<UsersController> logger)
            : base(logger)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] User model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new CustomArgumentException(ModelState.ToString());
                }

                await this._userManager.SignUp(model);
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

        [HttpPost]
        public async Task<ActionResult<SignIn>> SignIn([FromBody] User model)
        {
            SignIn signIn = null;

            try
            {
                signIn = await this._userManager.SignIn(model);
                HttpContext.Session.SetString("_UserEmail", model.Email); // Set user info to session for logging
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

            return Ok(signIn);
        }
    }
}