using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Repositories
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<IEnumerable<Feature>> GetFeaturesByFeatureTypeAsync(FeatureType featureType);
    }
}
