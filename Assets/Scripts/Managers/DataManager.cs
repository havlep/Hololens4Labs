using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;//TODO look into how to do this for the whole project


namespace HoloLens4Labs.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        public bool IsReady { get; private set; }

        [Header("Base Settings")]

        [SerializeField]
        private string connectionString = default;
        [SerializeField]
        private string experimentName = "MyAzurePowerToolsProject";

        [Header("Table Settings")]
        [SerializeField]
        private string experimentsTableName = "experiments";
        [SerializeField]
        private string photosTableName = "photos";
        [SerializeField]
        private string partitionKey = "main";
        [SerializeField]
        private bool tryCreateTableOnStart = true;

        [Header("Blob Settings")]
        [SerializeField]
        private string blockBlobContainerName = "tracked-photos-thumbnails";
        [SerializeField]
        private bool tryCreateBlobContainerOnStart = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onDataManagerReady = default;
        [SerializeField]
        private UnityEvent onDataManagerInitFailed = default;

        private CloudStorageAccount storageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable experimentsTable;
        private CloudTable photosTable;
        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        private async void Awake()
        {
            storageAccount = CloudStorageAccount.Parse(connectionString);
            cloudTableClient = storageAccount.CreateCloudTableClient();
            experimentsTable = cloudTableClient.GetTableReference(experimentsTableName);
            photosTable = cloudTableClient.GetTableReference(photosTableName);
            if (tryCreateTableOnStart)
            {
                try
                {
                    if (await experimentsTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {experimentsTableName}.");
                    }
                    if (await photosTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {photosTableName}.");
                    }
                }
                catch (StorageException ex)
                {
                    Debug.LogError("Failed to connect with Azure Storage.\nIf you are running with the default storage emulator configuration, please make sure you have started the storage emulator.");
                    Debug.LogException(ex);
                    onDataManagerInitFailed?.Invoke();
                }
            }

            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(blockBlobContainerName);
            if (tryCreateBlobContainerOnStart)
            {
                try
                {
                    if (await blobContainer.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created container {blockBlobContainerName}.");
                    }
                }
                catch (StorageException ex)
                {
                    Debug.LogError("Failed to connect with Azure Storage.\nIf you are running with the default storage emulator configuration, please make sure you have started the storage emulator.");
                    Debug.LogException(ex);
                    onDataManagerInitFailed?.Invoke();
                }
            }

            IsReady = true;
            onDataManagerReady?.Invoke();
        }

        /// <summary>
        /// Get a experiment or create one if it does not exist.
        /// </summary>
        /// <returns>Experiment instance from database.</returns>
        public async Task<Experiment> GetOrCreateExperiment()
        {
            var query = new TableQuery<Experiment>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, experimentName)));
            var segment = await experimentsTable.ExecuteQuerySegmentedAsync(query, null);

            var experiment = segment.Results.FirstOrDefault();
            if (experiment != null)
            {
                return experiment;
            }

            experiment = new Experiment()
            {
                Name = experimentName,
                RowKey = experimentName,
                PartitionKey = partitionKey,
           
            };

            var insertOrMergeOperation = TableOperation.InsertOrMerge(experiment);
            await experimentsTable.ExecuteAsync(insertOrMergeOperation);

            return experiment;
        }

        /// <summary>
        /// Update the experiment changes back to the table store;
        /// </summary>
        public async Task<bool> UpdateExperiment(Experiment experiment)
        {
            var insertOrMergeOperation = TableOperation.InsertOrMerge(experiment);
            var result = await experimentsTable.ExecuteAsync(insertOrMergeOperation);

            return result.Result != null;
        }

        /// <summary>
        /// Insert a new or update an PhotoExperiment instance on the table storage.
        /// </summary>
        /// <param name="photo">Instance to write or update.</param>
        /// <returns>Success result.</returns>
        public async Task<bool> UploadOrUpdate(Photo photo)
        {
            if (string.IsNullOrWhiteSpace(photo.PartitionKey))
            {
                photo.PartitionKey = partitionKey;
            }

            var insertOrMergeOperation = TableOperation.InsertOrMerge(photo);
            var result = await photosTable.ExecuteAsync(insertOrMergeOperation);

            return result.Result != null;
        }

        /// <summary>
        /// Get all PhotoExperiments from the table.
        /// </summary>
        /// <returns>List of all PhotoExperiments from table.</returns>
        public async Task<List<Photo>> GetAllPhotos()
        {
            var query = new TableQuery<Photo>();
            var segment = await photosTable.ExecuteQuerySegmentedAsync(query, null);

            return segment.Results;
        }

        /// <summary>
        /// Find a PhotoExperiment by a given Id (partition key).
        /// </summary>
        /// <param name="id">Id/Partition Key to search by.</param>
        /// <returns>Found PhotoExperiment, null if nothing is found.</returns>
        public async Task<Photo> FindPhotoById(string id)
        {
            var retrieveOperation = TableOperation.Retrieve<Photo>(partitionKey, id);
            var result = await photosTable.ExecuteAsync(retrieveOperation);
            var photo = result.Result as Photo;

            return photo;
        }

        /// <summary>
        /// Find a PhotoExperiment by its name.
        /// </summary>
        /// <param name="photoName">Name to search by.</param>
        /// <returns>Found PhotoExperiment, null if nothing is found.</returns>
        public async Task<Photo> FindPhotoByName(string photoName)
        {
            var query = new TableQuery<Photo>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, photoName)));
            var segment = await photosTable.ExecuteQuerySegmentedAsync(query, null);

            return segment.Results.FirstOrDefault();
        }

        /// <summary>
        /// Delete a PhotoExperiment from the table.
        /// </summary>
        /// <param name="instance">Object to delete.</param>
        /// <returns>Success result of deletion.</returns>
        public async Task<bool> DeletePhoto(Photo instance)
        {
            var deleteOperation = TableOperation.Delete(instance);
            var result = await photosTable.ExecuteAsync(deleteOperation);

            return result.HttpStatusCode == (int)HttpStatusCode.OK;
        }

        /// <summary>
        /// Upload data to a blob.
        /// </summary>
        /// <param name="data">Data to upload.</param>
        /// <param name="blobName">Name of the blob, ideally with file type.</param>
        /// <returns>Uri to the blob.</returns>
        public async Task<string> UploadBlob(byte[] data, string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);

            return blockBlob.Name;
        }

        /// <summary>
        /// Download data from a blob.
        /// </summary>
        /// <param name="blobName">Name of the blob.</param>
        /// <returns>Data as byte array.</returns>
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
        /// Delete a blob if it exists.
        /// </summary>
        /// <param name="blobName">Name of the blob to delete.</param>
        /// <returns>Success result of deletion.</returns>
        public async Task<bool> DeleteBlob(string blobName)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            return await blockBlob.DeleteIfExistsAsync();
        }
    }
}
