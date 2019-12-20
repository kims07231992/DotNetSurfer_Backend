using DotNetSurfer_Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface IHeaderManager
    {
        Task<IEnumerable<Header>> GetSideHeaderMenus();
    }
}
