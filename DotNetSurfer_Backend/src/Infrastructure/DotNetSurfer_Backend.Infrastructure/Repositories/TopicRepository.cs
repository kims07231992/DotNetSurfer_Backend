using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Interfaces.CDNs;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetSurfer_Backend.Infrastructure.Repositories
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        private readonly ICdnHandler _cdnHandler;

        public TopicRepository(DotNetSurferDbContext dbContext, ICdnHandler cdnHandler)
            : base(dbContext)
        {
            this._cdnHandler = cdnHandler;
        }

        public async Task<bool> IsTopicExistAsync(int id)
        {
            return await this._context.Topics.AnyAsync(t => t.TopicId == id);
        }

        public async Task<bool> IsTitleExistAsync(string title)
        {
            return await this._context.Topics.AnyAsync(t => t.Title == title);
        }

        public async Task<Topic> GetTopicAsync(int id)
        {
            return await this._context.Topics
                .Include(t => t.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.TopicId == id);
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            return await this._context.Topics
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByUserIdAsync()
        {
            return await this._context.Topics
                        .AsNoTracking()
                        .Select(t => new Topic
                        {
                            TopicId = t.TopicId,
                            Title = t.Title,
                            Description = t.Description,
                            PictureUrl = t.PictureUrl,
                            PostDate = t.PostDate,
                            ShowFlag = t.ShowFlag
                        })
                        .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByUserIdAsync(int userId)
        {
            return await this._context.Topics
                    .Where(a => a.UserId == userId)
                    .AsNoTracking()
                    .Select(t => new Topic
                    {
                        TopicId = t.TopicId,
                        Title = t.Title,
                        Description = t.Description,
                        PictureUrl = t.PictureUrl,
                        PostDate = t.PostDate,
                        ShowFlag = t.ShowFlag
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetSideHeaderMenusAsync()
        {
            return await this._context.Topics
                    .Where(t => t.ShowFlag)
                    .AsNoTracking()
                    .Select(t => new Topic
                    {
                        TopicId = t.TopicId,
                        Title = t.Title,
                        Articles = t.Articles == null 
                            ? null 
                            : t.Articles.Select(a => new Article
                            {
                                ArticleId = a.ArticleId,
                                Title = a.Title
                            }).ToList()
                    }).ToListAsync();
        }

        public override void Create(Topic entity)
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

        public override void Update(Topic entity)
        {
            entity.ModifyDate = DateTime.Now;

            if (entity.Picture != null)
            {
                string url = this._context.Topics
                    .AsNoTracking()
                    .First(t => t.TopicId == entity.TopicId)
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

        public override void Delete(Topic entity)
        {
            string url = this._context.Topics
                .AsNoTracking()
                .First(t => t.TopicId == entity.TopicId)
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
