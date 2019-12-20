using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Managers
{
    public interface ILogManager
    {
        Task WriteErrorLog(string message);

        Task WriteInfoLog(string message);
    }
}
