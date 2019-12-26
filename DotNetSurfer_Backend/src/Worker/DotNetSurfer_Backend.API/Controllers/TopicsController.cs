using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Managers;

namespace DotNetSurfer_Backend.API.Controllers
{
    public class TopicsController : BaseController<TopicsController>
    {
        private readonly ITopicManager _topicManager;

        public TopicsController(ITopicManager topicManager, ILogger<TopicsController> logger)
            : base(logger)
        {
            this._topicManager = topicManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic([FromRoute] int id)
        {
            Topic topic = null;

            try
            {
                topic = await this._topicManager.GetTopic(id);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        {
            IEnumerable<Topic> topics = null;

            try
            {
                topics = await this._topicManager.GetTopics();
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

            return Ok(topics);
        }

        [HttpGet("users/{userId}")]
        [Authorize(Roles = nameof(PermissionType.Admin) + "," + nameof(PermissionType.User))]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByUserId([FromRoute] int userId)
        {
            IEnumerable<Topic> topics = null;

            try
            {
                topics = await this._topicManager.GetTopicsByUserId(userId, User);
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

            return Ok(topics);
        }
    }
}