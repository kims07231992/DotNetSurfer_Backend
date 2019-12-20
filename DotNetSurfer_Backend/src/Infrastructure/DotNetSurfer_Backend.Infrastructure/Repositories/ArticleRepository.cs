using DotNetSurfer_Backend.Core.Interfaces.CDNs;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Infrastructure.Entities;
using DotNetSurfer_Backend.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Infrastructure.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        private readonly ICdnHandler _cdnHandler;

        public ArticleRepository(DotNetSurferDbContext dbContext, ICdnHandler cdnHandler) 
            : base(dbContext)
        {
            this._cdnHandler = cdnHandler;
        }

        public async Task<bool> IsArticleExistAsync(int id)
        {
            return await this._context.Articles.AnyAsync(a => a.ArticleId == id);
        }

        public async Task<Article> GetArticleAsync(int id)
        {
            return await this._context.Articles
                 .Include(a => a.User)
                 .Include(a => a.Topic)
                 .Include(a => a.Tags)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(a => a.ArticleId == id);
        }

        public async Task<Article> GetArticleDetailAsync(int id)
        {
            return await this._context.Articles
                .Include(a => a.Topic)
                .AsNoTracking()
                .Select(a => new Article
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Content = a.Content,
                    Topic = new Topic
                    {
                        Title = a.Topic.Title
                    }
                })
                .SingleOrDefaultAsync(a => a.ArticleId == id);
        }

        public async Task<IEnumerable<Article>> GetArticlesByUserIdAsync(int contentLength)
        {
            var articles = await this._context.Articles
                .Select(a => new Article
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.Category,
                    PictureUrl = a.PictureUrl,
                    PostDate = a.PostDate,
                    ShowFlag = a.ShowFlag
                })
                .AsNoTracking()
                .ToListAsync();

            articles?.ForEach(a =>
            {
                a.Content = a.Content.ToTrimmedPlainTextFromHtml(contentLength);
            });

            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticlesByUserIdAsync(int userId, int contentLength)
        {
            var articles = await this._context.Articles
                .Where(a => a.UserId == userId)
                .Select(a => new Article
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.Category,
                    PictureUrl = a.PictureUrl,
                    PostDate = a.PostDate,
                    ShowFlag = a.ShowFlag
                })
                .AsNoTracking()
                .ToListAsync();

            articles?.ForEach(a =>
            {
                a.Content = a.Content.ToTrimmedPlainTextFromHtml(contentLength);
            });

            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticlesByPageAsync(int pageId, int itemPerPage)
        {
            return await this._context.Articles
                    .Include(a => a.Topic)
                    .Where(a => a.ShowFlag)
                    .OrderByDescending(a => a.PostDate)
                    .Skip((pageId - 1) * itemPerPage)
                    .Take(itemPerPage)
                    .AsNoTracking()
                    .Select(a => new Article
                    {
                        ArticleId = a.ArticleId,
                        Title = a.Title,
                        PictureUrl = a.PictureUrl,
                        PostDate = a.PostDate,
                        Topic = new Topic
                        {
                            Title = a.Topic.Title
                        }
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetTopArticlesAsync(int item, int contentLength)
        {
            var articles = await this._context.Articles
                    .Include(a => a.User)
                    .Where(a => a.ShowFlag)
                    .OrderByDescending(a => a.ReadCount)
                    .Take(item)
                    .AsNoTracking()
                    .Select(a => new Article
                    {
                        ArticleId = a.ArticleId,
                        Title = a.Title,
                        Content = a.Content,
                        PictureUrl = a.PictureUrl,
                        PostDate = a.PostDate,
                        User = new User
                        {
                            Name = a.User.Name,
                            PictureUrl = a.User.PictureUrl
                        }
                    })
                    .ToListAsync();

            articles?.ForEach(a =>
            {
                a.Content = a.Content.ToTrimmedPlainTextFromHtml(contentLength);
            });

            return articles;
        }

        public async Task IncreaseArticleReadCountAsync(int id)
        {
            var article = await _context.Articles.FirstAsync(a => a.ArticleId == id);
            article.ReadCount++;
        }

        public override void Create(Article entity)
        {        
            if (entity.Picture != null)
            {
                string fileName = Guid.NewGuid().ToString();
                Task upload = this._cdnHandler.UpsertImageToStorageAsync(entity.Picture, fileName);

                string imageUrl = this._cdnHandler.GetImageStorageBaseUrl().Result + fileName;
                entity.Picture = null;
                entity.PictureMimeType = null;
                entity.PictureUrl = imageUrl;
                base.Create(entity);

                upload.GetAwaiter().GetResult();
            }
            else
            {
                base.Create(entity);
            }
        }

        public override void Update(Article entity)
        {
            entity.ModifyDate = DateTime.Now;

            if (entity.Picture != null)
            {
                string url = this._context.Articles
                    .AsNoTracking()
                    .First(a => a.ArticleId == entity.ArticleId)
                    .PictureUrl;
                string imageStorageBaseUrl = this._cdnHandler.GetImageStorageBaseUrl().Result;
                string fileName = string.IsNullOrEmpty(url) 
                    ? Guid.NewGuid().ToString() // Create case
                    : url.Replace(imageStorageBaseUrl, string.Empty); // Update upload case


                Task upload = this._cdnHandler.UpsertImageToStorageAsync(entity.Picture, fileName);
                string imageUrl = imageStorageBaseUrl + fileName;
                entity.Picture = null;
                entity.PictureMimeType = null;
                entity.PictureUrl = imageUrl;
                base.Update(entity);

                upload.GetAwaiter().GetResult();
            }
            else
            {
                base.Update(entity);
            }
        }

        public override void Delete(Article entity)
        {
            string url = this._context.Articles
                .AsNoTracking()
                .First(a => a.ArticleId == entity.ArticleId)
                .PictureUrl;
            if (!string.IsNullOrEmpty(url))
            {
                string imageStorageBaseUrl = this._cdnHandler.GetImageStorageBaseUrl().Result;
                string fileName = url.Replace(imageStorageBaseUrl, string.Empty);

                Task delete = this._cdnHandler.DeleteImageFromStorageAsync(fileName);
                base.Delete(entity);

                delete.GetAwaiter().GetResult();
            }
            else
            {
                base.Delete(entity);
            }      
        }
    }
}
