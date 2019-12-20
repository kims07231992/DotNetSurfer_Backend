using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DotNetSurferDbContext _context { get; set; }

        public BaseRepository(DotNetSurferDbContext context)
        {
            this._context = context;
        }

        public virtual void Create(T entity)
        {
            this._context.Add<T>(entity);
        }

        public virtual void Update(T entity)
        {
            this._context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            this._context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
