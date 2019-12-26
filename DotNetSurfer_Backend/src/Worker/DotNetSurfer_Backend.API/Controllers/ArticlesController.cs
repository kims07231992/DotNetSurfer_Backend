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
    public class ArticlesController : BaseController<ArticlesController>
    {
        private readonly int _itemPerPage;
        private readonly int _tableContentLength; // plainText length to show
        private readonly int _cardContentLength; // plainText length to show
        private readonly IArticleManager _articleManager;

        public ArticlesController(IArticleManager articleManager, ILogger<ArticlesController> logger)
            : base(logger)
        {
            this._itemPerPage = 3;
            this._tableContentLength = 100;
            this._cardContentLength = 50;
            this._articleManager = articleManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle([FromRoute] int id)
        {
            Article article = null;

            try
            {
                article = await this._articleManager.GetArticle(id);
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

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<Article>> GetArticleDetail([FromRoute]int id)
        {
            Article article = null;

            try
            {
                article = await this._articleManager.GetArticleDetail(id);
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

        [HttpGet("users/{userId}")]
        [Authorize(Roles = nameof(PermissionType.Admin) + "," + nameof(PermissionType.User))]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesByUserIdAsync([FromRoute] int userId)
        {
            IEnumerable<Article> articles = null;

            try
            {
                articles = await this._articleManager.GetArticlesByUserIdAsync(userId, this._tableContentLength, User);
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

            return Ok(articles);
        }

        [HttpGet("page/{pageId?}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesByPage(int pageId = 1)
        {
            IEnumerable<Article> articles = null;

            try
            {
                articles = await this._articleManager.GetArticlesByPage(pageId, this._itemPerPage);
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

            return Ok(articles);
        }

        [HttpGet("top/{item?}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetTopArticles(int item = 3)
        {
            IEnumerable<Article> articles = null;

            try
            {
                articles = await this._articleManager.GetTopArticles(this._itemPerPage, this._cardContentLength);
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

            return Ok(articles);
        }
    }
}