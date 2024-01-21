// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories
{

    /// <summary>
    /// Interface for the BlobRepository
    /// </summary>
    public interface BlobRepositoryInterface
    {
        /// <summary>
        /// Upload data to a blob.
        /// </summary>
        /// <param name="data">Data to upload.</param>
        /// <param name="blobName">Name of the blob, ideally with file type.</param>
        /// <returns>Uri to the blob.</returns>
        public Task<string> UploadBlob(byte[] data, string blobName);

        /// <summary>
        /// Download data from a blob.
        /// </summary>
        /// <param name="blobName">Name of the blob.</param>
        /// <returns>Data as byte array.</returns>
        public Task<byte[]> DownloadBlob(string blobName);

        /// <summary>
        /// Delete a blob if it exists.
        /// </summary>
        /// <param name="blobName">Name of the blob to delete.</param>
        /// <returns>Success result of deletion.</returns>
        public Task<bool> DeleteBlob(string blobName);

    }
}