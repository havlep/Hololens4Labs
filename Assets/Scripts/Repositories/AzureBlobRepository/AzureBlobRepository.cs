using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;


namespace HoloLens4Labs.Scripts.Repositories.AzureBlob
{
    public class AzureBlobRepository : BlobRepositoryInterface
    {

        private CloudBlobContainer blobContainer;

        public AzureBlobRepository(CloudBlobContainer blobContainer)
        {
            this.blobContainer = blobContainer;
        }
        public async Task<bool> DeleteBlob(string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            return await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<byte[]> DownloadBlob(string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            using (var stream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                return stream.ToArray();
            }
        }

        public async Task<string> UploadBlob(byte[] data, string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);

            return blockBlob.Name;
        }
    }
}