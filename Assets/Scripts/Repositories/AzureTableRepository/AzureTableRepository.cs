using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Repositories.AzureBlob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    /// <summary>
    /// A concrete implementation of the repository interface for the Azure Table service and Azure Blob service
    /// </summary>
    public class AzureTableRepository : MonoBehaviour, RepositoryInterface
    {


        public bool IsReady { get; private set; }

        [Header("Base Settings")]
        [SerializeField]
        private string connectionString = default;

        [Header("Table Settings")]
        [SerializeField]
        private string experimentsTableName = "experiments";
        [SerializeField]
        private string scientistsTableName = "scientists";
        [SerializeField]
        private string logsTableName = "logs";
        [SerializeField]
        private string partitionKey = "main";
        [SerializeField]
        private bool tryCreateTableOnStart = true;

        [Header("Blob Settings")]
        [SerializeField]
        private string blockBlobContainerName = "log-images";
        [SerializeField]
        private bool tryCreateBlobContainerOnStart = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onRepoReadyReady = default;
        [SerializeField]
        private UnityEvent onRepoReadyInitFailed = default;

        private CloudStorageAccount storageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable experimentsTable;

        private CloudTable logsTable;

        private CloudTable scientistsTable;

        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        private ATExperimentRepository atExperimentRepository;
        private ATScientistRepository atScientistRepository;
        private ATLogRepository atLogRepository;

        private AzureBlobRepository blobRepository;

        /// <summary>
        /// Initialize the repository when the unity object is created
        /// </summary>
        private async void Awake()
        {

            storageAccount = CloudStorageAccount.Parse(connectionString);

            await this.SetupTablesRepository();
            this.SetupBlobRepository();

            Debug.Log($"Azure repository intialized.");
            IsReady = true;
            onRepoReadyReady?.Invoke();

        }

        /// <summary>
        /// Setup the tables repository for all model objects
        /// </summary>
        /// <returns>True on success</returns>
        /// <exception cref="StorageException">If the connection to the Azure Storage fails</exception>"
        private async Task<bool> SetupTablesRepository()
        {
            if(atExperimentRepository != null && atScientistRepository != null && atLogRepository != null)
                return true;

            cloudTableClient = storageAccount.CreateCloudTableClient();
            experimentsTable = cloudTableClient.GetTableReference(experimentsTableName);
            scientistsTable = cloudTableClient.GetTableReference(scientistsTableName);
            logsTable = cloudTableClient.GetTableReference(logsTableName);

            if (tryCreateTableOnStart)
            {
                try
                {
                    if (await experimentsTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {experimentsTableName}.");
                    }
                    if (await scientistsTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {scientistsTableName}.");
                    }
                    if (await logsTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {logsTableName}.");
                    }

                }
                catch (StorageException ex)
                {
                    Debug.LogError("Failed to connect with Azure Storage.\nIf you are running with the default storage emulator configuration, please make sure you have started the storage emulator.");
                    Debug.LogException(ex);
                    onRepoReadyInitFailed?.Invoke();
                }
            }

            // Setup the repository objects that will be used to get data
            atExperimentRepository = new ATExperimentRepository(experimentsTable, partitionKey);
            atScientistRepository = new ATScientistRepository(scientistsTable, partitionKey);

            if (blobRepository == null)
                SetupBlobRepository();

            atLogRepository = new ATLogRepository(logsTable, partitionKey, blobRepository.DownloadBlob);
            return true;

        }

        /// <summary>
        /// Setup the blob repository for holding the image data
        /// </summary>
        /// <returns>True on success</returns>
        /// <exception cref="StorageException">If the connection to the Azure Blob Storage fails</exception>""
        private bool SetupBlobRepository()
        {
            if (blobRepository != null)
                return true;
            
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(blockBlobContainerName);

            if (tryCreateBlobContainerOnStart)
            {
                try
                {
                    if (blobContainer.CreateIfNotExistsAsync().Result)
                    {
                        Debug.Log($"Created blob container {blockBlobContainerName}.");
                    }
                }
                catch (StorageException ex)
                {
                    Debug.LogError("Failed to connect with Azure Storage.\nIf you are running with the default storage emulator configuration, please make sure you have started the storage emulator.");
                    Debug.LogException(ex);
                    onRepoReadyInitFailed?.Invoke();
                }
            }
            blobRepository = new AzureBlobRepository(blobContainer);
            return true;

        }

        /// <summary>
        /// Create a new experiment
        /// </summary>
        /// <param name="experiment">The Experiment that will be created in the repository</param>
        /// <returns>The experiment as returned by the repository</returns>
        /// <exception cref="ObjectNotInitializedException">If the scientist was not initialized for the experiment</exception>
        public async Task<Experiment> CreateExperiment(Experiment experiment)
        {

            if (experiment.CreatedByID == string.Empty)
            {
                if (experiment.CreatedBy != null)
                    experiment.CreatedBy = await CreateScientist(experiment.CreatedBy);
                else
                    throw new ObjectNotInitializedException("Scientist not intialized for experiment");
            }

            return await atExperimentRepository.Create(experiment);


        }

        /// <summary>
        /// Update an experiment in the repository
        /// </summary>
        /// <param name="experiment">The experiment that will be updated</param>
        /// <returns>True on success</returns>
        public async Task<bool> UpdateExperiment(Experiment experiment)
        {

            return await atExperimentRepository.Update(experiment);

        }

        /// <summary>
        /// Create a new scientist in the repository
        /// </summary>
        /// <param name="scientist"> The scientist that will be created in the repository </param>
        /// <returns> The scientist as returned by the repository </returns>
        public async Task<Scientist> CreateScientist(Scientist scientist)
        {
            return await atScientistRepository.Create(scientist);

        }

        /// <summary>
        /// Update a scientist in the repository
        /// </summary>
        /// <param name="scientist"> The scientist that will be updated in the repository </param>
        /// <returns> true on sucess </returns>
        public async Task<bool> UpdateScientist(Scientist scientist)
        {

            return await atScientistRepository.Update(scientist);

        }

        /// <summary>
        /// Create a new log in the repository
        /// </summary>
        /// <param name="log"> The log that will be created in the repository </param>
        /// <returns> The log as returned by the repository after creation </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Log> CreateLog(Log log)
        {

            if (log.Id == string.Empty)
                log.Id = System.Guid.NewGuid().ToString();

            if (log is TextLog
                && (log as TextLog).TextData != null
                && (log as TextLog).TextData.Id == string.Empty)
            {
                (log as TextLog).TextData.Id = System.Guid.NewGuid().ToString();
            }
            else if (log is ImageLog && (log as ImageLog).Data != null)
            {
                var imageLog = log as ImageLog;

                if (imageLog.Data.Id == string.Empty)
                    imageLog.Data.Id = System.Guid.NewGuid().ToString();

                var data = await imageLog.Data.getData();
                imageLog.Data.Id = await blobRepository.UploadBlob(data, imageLog.Data.Id);

            }
            else
                throw new System.NotImplementedException();

            return await atLogRepository.Create(log);

        }


        /// <summary>
        /// Update a log in the repository
        /// </summary>
        /// <param name="log"> The log that will be updated in the repository </param>
        /// <returns>True on success</returns>
        public async Task<bool> UpdateLog(Log log)
        {

            if (log is ImageLog)
            {
                var imageLog = log as ImageLog;
                if (imageLog.Data != null)
                {
                    var data = await imageLog.Data.getData();
                    imageLog.Data.Id = await blobRepository.UploadBlob(data, imageLog.Data.Id);
                }
            }
            
            return await atLogRepository.Update(log);

        }

        /// <summary>
        /// Delete an experiment from the repository
        /// </summary>
        /// <param name="experiment">The Experiment that will be deleted</param>
        /// <returns>True if the experiment was deleted</returns>
        public Task<bool> DeleteExperiment(Experiment experiment)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete a scientist from the repository
        /// </summary>
        /// <param name="scientist">The Scientist that will be deleted</param>
        /// <returns>True if the Scientist was deleted</returns>
        public Task<bool> DeleteScientist(Scientist scientist)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete a log from the table repository and any associated blobs from the blob repository
        /// </summary>
        /// <param name="log">The Log that will be deleted</param>
        /// <returns>True if the Log was deleted</returns>
        /// <exception cref="ObjectDataBaseException">If the blob could not be deleted</exception>"
        public async Task<bool> DeleteLog(Log log)
        {

            if (log is ImageLog)
            {

                var imageLog = log as ImageLog;
                if (imageLog.Data != null &&
                    !await blobRepository.DeleteBlob(imageLog.Data.Id))
                    throw new ObjectDataBaseException($"Could not delete image{imageLog.Data.Id}");

            }
            else if (log is TranscriptionLog)
            {
                var transcriptionLog = log as TranscriptionLog;
                if (transcriptionLog.Data != null &&
                                       !await blobRepository.DeleteBlob(transcriptionLog.Data.Id))
                    throw new ObjectDataBaseException($"Could not delete transcription{transcriptionLog.Data.Id}");
            }
            throw new System.NotImplementedException();

        }

        /// <summary>
        /// A method used for checking if the repository is ready
        /// </summary>
        /// <returns> True if all repositories are ready</returns>
        bool RepositoryInterface.IsReady()
        {
            return IsReady;
        }

        /// <summary>
        /// Get a page of 100 logs for an experiment
        /// </summary>
        /// <param name="experimentID"> The ID of the experiment </param>
        /// <param name="token">The continuation token for the pagination session </param>
        /// <returns> An array of logs and a continuation token </returns>
        public async Task<(Log[], TableContinuationToken)> GetLogsForExperiment(string experimentID, TableContinuationToken token)
        {


            return await atLogRepository.GetLogsForExperiment(experimentID, 100, token);


        }
        ///<summary>
        /// Get all logs for an experiment
        /// </summary>
        /// <param name="experimentID"> The ID of the experiment </param>
        /// <returns> An array of logs </returns>
        public async Task<Log[]> GetLogsForExperiment(string experimentID)
        {
            var (logs, token) = await GetLogsForExperiment(experimentID, null);
            return logs;
        }

        /// <summary>
        /// Get a page of 100 experiments
        /// </summary>
        /// <param name="token">The continuation token for the pagination session </param>
        /// <returns>A list of experiments and a pagination token</returns>
        public async Task<(Experiment[], TableContinuationToken)> GetAllExperiments(TableContinuationToken token)
        {
            return await atExperimentRepository.ReadOnePageAsync(100, token);
        }

        /// <summary>
        /// Get all experiments
        /// </summary>
        /// <returns>A list of max 100 experiments</returns>

        public async Task<Experiment[]> GetAllExperiments()
        {
            var (logs, token) = await GetAllExperiments(null);
            return logs;
        }
    }
}
