using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Repositories
{
    public interface IStatusRepository : IRepository<Status>
    {
        Task<IEnumerable<Status>> GetStatusesAsync();
    }
}
