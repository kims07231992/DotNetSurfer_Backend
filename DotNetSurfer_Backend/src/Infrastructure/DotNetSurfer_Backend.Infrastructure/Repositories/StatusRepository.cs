using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Infrastructure.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(DotNetSurferDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await this._context.Statuses.ToListAsync();
        }
    }
}
