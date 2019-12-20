using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace DotNetSurfer_Backend.Core.Managers
{
    public abstract class BaseManager<T> where T : BaseManager<T>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<T> _logger;

        public BaseManager(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        protected bool IsAdministrator(ClaimsPrincipal claimsPrincipal)
        {
            var roleClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            return roleClaim?.Value == nameof(PermissionType.Admin) ? true : false;
        }

        protected int? GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            int? userId = string.IsNullOrEmpty(userIdClaim?.Value) ? (int?)null : Convert.ToInt32(userIdClaim.Value);
            return userId;
        }
    }
}
