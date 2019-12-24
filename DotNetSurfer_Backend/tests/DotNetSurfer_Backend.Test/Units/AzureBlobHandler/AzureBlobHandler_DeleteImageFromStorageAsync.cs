using DotNetSurfer_Backend.Core.Interfaces.CDNs;
using Xunit;

namespace DotNetSurfer_Backend.Test.Units.AzureBlobHandler
{
    public class AzureBlobHandler_DeleteImageFromStorageAsync
    {
        private readonly ICdnHandler _cdnHandler;
        private readonly string _fileName;

        public AzureBlobHandler_DeleteImageFromStorageAsync()
        {
            this._cdnHandler = AzureBlobTestHelper.GetAzureBlobHandler();
            this._fileName = AzureBlobTestHelper.GetTestFileName();
        }

        [Fact]
        public void Delete_Image_ReturnsTrue()
        {
            bool isSuccess = this._cdnHandler.DeleteImageFromStorageAsync(this._fileName).Result;

            Assert.True(isSuccess);
        }
    }
}
