using DotNetSurfer_Backend.Core.Interfaces.CDNs;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Infrastructure.CDNs
{
    public class AzureBlobHandler : ICdnHandler
    {
        // Azure blob only allows lowercase
        public enum ContainerType { images };

        private readonly string _accountName;
        private readonly string _accountKey;
        private readonly string _imageStorageBaseUrl;

        #region Constructors
        public AzureBlobHandler(string accountName, string accountKey)
        {
            this._accountName = accountName;
            this._accountKey = accountKey;
        }

        public AzureBlobHandler(IConfiguration configuration)
        {
            this._accountName = configuration["Blob:AccountName"];
            this._accountKey = configuration["Blob:AccountKey"];
            this._imageStorageBaseUrl = $"{configuration["Blob:BaseUrl"]}{ContainerType.images.ToString()}/";
        }
        #endregion

        #region Public
        public async Task<string> GetImageStorageBaseUrl()
        {
            return await Task.FromResult(this._imageStorageBaseUrl);
        }

        public async Task<bool> UpsertImageToStorageAsync(byte[] binaryFile, string fileName)
        {
            var blobContainer = GetBlobContainer(ContainerType.images);
            var blockBlob = blobContainer.GetBlockBlobReference(fileName);

            bool isSucess = await UploadBinaryFileToStorageAsync(binaryFile, fileName, blockBlob);
            if (!isSucess)
            {
                throw new AzureBlobFileUploadException();
            }

            return await Task.FromResult(isSucess);
        }

        public async Task<bool> DeleteImageFromStorageAsync(string fileName)
        {
            var blobContainer = GetBlobContainer(ContainerType.images);
            var blockBlob = blobContainer.GetBlockBlobReference(fileName);

            bool isSucess = await DeleteFileFromStorageAsync(fileName, blockBlob);
            if (!isSucess)
            {
                throw new AzureBlobFileDeleteException();
            }

            return await Task.FromResult(isSucess);
        }
        #endregion

        #region Private
        private CloudBlobContainer GetBlobContainer(ContainerType containerType)
        {
            var storageCredentials = new StorageCredentials(this._accountName, this._accountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient.GetContainerReference(containerType.ToString());
        }

        private async Task<bool> UploadBinaryFileToStorageAsync(byte[] binaryFile, string fileName, CloudBlockBlob blockBlob)
        {
            await blockBlob.UploadFromByteArrayAsync(binaryFile, 0, binaryFile.Length);

            return await Task.FromResult(true);
        }

        private async Task<bool> DeleteFileFromStorageAsync(string fileName, CloudBlockBlob blockBlob)
        {
            bool isSuccess = await blockBlob.DeleteIfExistsAsync();

            return await Task.FromResult(isSuccess);
        }
        #endregion

        #region Exceptions
        private class AzureBlobFileUploadException : Exception
        {
            public AzureBlobFileUploadException()
            {

            }

            public AzureBlobFileUploadException(string message)
                : base(message)
            {

            }

            public AzureBlobFileUploadException(string message, Exception inner)
                : base(message, inner)
            {

            }
        }

        private class AzureBlobFileDeleteException : Exception
        {
            public AzureBlobFileDeleteException()
            {

            }

            public AzureBlobFileDeleteException(string message)
                : base(message)
            {

            }

            public AzureBlobFileDeleteException(string message, Exception inner)
                : base(message, inner)
            {

            }
        }
        #endregion
    }
}
