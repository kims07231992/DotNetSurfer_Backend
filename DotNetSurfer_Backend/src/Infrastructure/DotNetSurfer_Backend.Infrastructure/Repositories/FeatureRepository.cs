using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Infrastructure.Repositories
{
    public class FeatureRepository : BaseRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(DotNetSurferDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<Feature>> GetFeaturesByFeatureTypeAsync(FeatureType featureType)
        {
            return await this._context.Features
                .Where(f => f.FeatureType == featureType)
                .ToListAsync();                  
        }
    }
}
