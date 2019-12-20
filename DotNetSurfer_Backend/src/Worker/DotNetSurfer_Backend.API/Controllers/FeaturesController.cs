using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Exceptions;

namespace DotNetSurfer_Backend.API.Controllers
{
    public class FeaturesController : BaseController<FeaturesController>
    {
        private readonly IFeatureManager _featureManager;

        public FeaturesController(IFeatureManager featureManager, ILogger<FeaturesController> logger)
            : base(logger)
        {
            this._featureManager = featureManager;
        }

        [HttpGet("{featureType}")]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeaturesByFeatureType([FromRoute] string featureType)
        {
            IEnumerable<Feature> features = null;

            try
            {
                features = await this._featureManager.GetFeaturesByFeatureType(featureType);
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

            return Ok(features);
        }
    }
}