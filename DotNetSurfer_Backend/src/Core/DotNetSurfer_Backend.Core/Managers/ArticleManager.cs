using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class ArticleManager : BaseManager<ArticleManager>, IArticleManager
    {
        public ArticleManager(IUnitOfWork unitOfWork, ILogger<ArticleManager> logger)
             : base(unitOfWork, logger)
        {

        }

        public async Task<Article> GetArticle(int id)
        {
            Article article = null;

            try
            {
                var entityModel = await this._unitOfWork.ArticleRepository
                    .GetArticleAsync(id);

                if (entityModel == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }

                article = entityModel;
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, id.ToString());
                throw new BaseCustomException();
            }

            return article;
        }

        public async Task<Article> GetArticleDetail(int id)
        {
            Article article = null;

            try
            {
                var entityModel = await this._unitOfWork.ArticleRepository
                    .GetArticleDetailAsync(id);

                if (entityModel == null)
                {
                    throw new CustomNotFoundException($"ID: {id}");
                }

                // Increase read count
                await this._unitOfWork.ArticleRepository.IncreaseArticleReadCountAsync(id);
                await this._unitOfWork.ArticleRepository.SaveAsync();

                article = entityModel;
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, id.ToString());
                throw new BaseCustomException();
            }

            return article;
        }

        public async Task<IEnumerable<Article>> GetArticlesByUserIdAsync(int userId, int tableContentLength, ClaimsPrincipal claimsPrincipal)
        {
            IEnumerable<Article> articles = null;

            try
            {
                bool isUserExist = await this._unitOfWork.UserRepository.IsUserExistAsync(userId);
                if (!isUserExist)
                {
                    return null;
                }

                var entityModels = IsAdministrator(claimsPrincipal)
                    ? await this._unitOfWork
                        .ArticleRepository.GetArticlesByUserIdAsync(tableContentLength)
                    : await this._unitOfWork
                        .ArticleRepository.GetArticlesByUserIdAsync(userId, tableContentLength); // Restrict by userId

                articles = entityModels?.Select(a => a);
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, userId.ToString());
                throw new BaseCustomException();
            }

            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticlesByPage(int pageId, int itemPerPage)
        {
            IEnumerable<Article> articles = null;

            try
            {
                var entityModels = await this._unitOfWork
                    .ArticleRepository
                    .GetArticlesByPageAsync(pageId, itemPerPage);

                articles = entityModels?.Select(a => a);
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"PageID: {pageId}, ItemPerPage: {itemPerPage}");
                throw new BaseCustomException();
            }

            return articles;
        }

        public async Task<IEnumerable<Article>> GetTopArticles(int itemPerPage, int cardContentLength)
        {
            IEnumerable<Article> articles = null;

            try
            {
                var entityModels = await this._unitOfWork
                    .ArticleRepository
                    .GetTopArticlesAsync(itemPerPage, cardContentLength);

                articles = entityModels?.Select(a => a);
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"ItemPerPage: {itemPerPage.ToString()}, CardContentLength: {cardContentLength.ToString()}");
                throw new BaseCustomException();
            }

            return articles;
        }
    }
}
