using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.CDNs
{
    public interface ICdnHandler
    {
        Task<string> GetImageStorageBaseUrl();
        Task<bool> UpsertImageToStorageAsync(byte[] binaryFile, string storedUrl);
        Task<bool> DeleteImageFromStorageAsync(string fileName);
    }
}
