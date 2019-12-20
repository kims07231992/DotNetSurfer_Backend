using DotNetSurfer_Backend.Core.Interfaces.CDNs;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ICdnHandler _cdnHandler;

        public UserRepository(DotNetSurferDbContext dbContext, ICdnHandler cdnHandler)
            : base(dbContext)
        {
            this._cdnHandler = cdnHandler;
        }

        public async Task<bool> IsUserExistAsync(int id)
        {
            return await this._context.Users
                .AnyAsync(u => u.UserId == id);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await this._context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await this._context.Users
                .SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this._context.Users
                    .Include(u => u.Permission)
                    .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserAsNoTrackingAsync(int id)
        {
            return await this._context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public override void Create(User entity)
        {
            base.Create(entity);
        }

        public override void Update(User entity)
        {
            if (entity.Picture != null)
            {
                string url = this._context.Users
                    .AsNoTracking()
                    .First(u => u.UserId == entity.UserId)
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

        public override void Delete(User entity)
        {
            string url = this._context.Users
                .AsNoTracking()
                .First(u => u.UserId == entity.UserId)
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
