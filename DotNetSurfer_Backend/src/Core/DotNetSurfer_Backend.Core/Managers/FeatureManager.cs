using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class FeatureManager : BaseManager<FeatureManager>, IFeatureManager
    {
        public FeatureManager(IUnitOfWork unitOfWork, ILogger<FeatureManager> logger)
           : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Feature>> GetFeaturesByFeatureType(string featureType)
        {
            IEnumerable<Feature> features = null;

            try
            {
                FeatureType featureTypeEnum;
                if (string.IsNullOrEmpty(featureType) || !Enum.TryParse(featureType, out featureTypeEnum))
                {
                    throw new CustomArgumentException($"FeatureType: {featureType}");
                }

                features = await this._unitOfWork.FeatureRepository
                    .GetFeaturesByFeatureTypeAsync(featureTypeEnum);
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, featureType);
                throw new BaseCustomException();
            }

            return features;
        }
    }
}
