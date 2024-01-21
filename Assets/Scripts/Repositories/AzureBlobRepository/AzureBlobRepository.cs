// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;


namespace HoloLens4Labs.Scripts.Repositories.AzureBlob
{
    /// <summary>
    /// An implementation of the BlobRepositoryInterface for the Azure Blob service
    /// </summary>
    public class AzureBlobRepository : BlobRepositoryInterface
    {

        /// <summary>
        /// The Azure Blob container that the repository is connected to
        /// </summary>
        private CloudBlobContainer blobContainer;

        /// <summary>
        /// Constructor for the AzureBlobRepository
        /// </summary>
        /// <param name="blobContainer">The Blob Container of the repository</param>
        public AzureBlobRepository(CloudBlobContainer blobContainer)
        {
            this.blobContainer = blobContainer;
        }

        /// <summary>
        /// Deletes a blob from the Blob Container
        /// </summary>
        /// <param name="blobName">The name of the blob to be deleted</param>
        /// <returns>True if the blob was deleted, false if it didn't exist</returns>
        public async Task<bool> DeleteBlob(string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            return await blockBlob.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Downloads a blob from the Blob Container
        /// </summary>
        /// <param name="blobName">The name of the blob to be downloaded</param>
        /// <returns>The byte array of the blob</returns>
        public async Task<byte[]> DownloadBlob(string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            using (var stream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Upload a blob to the Blob Container
        /// </summary>
        /// <returns>The name of the blob that was uploaded</returns>
        public async Task<string> UploadBlob(byte[] data, string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);

            return blockBlob.Name;
        }
    }
}