
using Codice.Client.BaseCommands.Ls;
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Repositories.AzureBlob;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;




using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;//TODO look into how to do this for the whole project



namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{

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


        private async void Awake()
        {
            
            storageAccount = CloudStorageAccount.Parse(connectionString);
            
            await this.SetupTablesRepository();
            this.SetupBlobRepository();

            Debug.Log($"Azure repository intialized.");
            IsReady = true;
            onRepoReadyReady?.Invoke();

        }

        private async Task<bool> SetupTablesRepository()
        {
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

            // Setup the services that will be used to get data

            atExperimentRepository = new ATExperimentRepository(experimentsTable, partitionKey);
            atScientistRepository = new ATScientistRepository(scientistsTable, partitionKey);
            atLogRepository = new ATLogRepository(logsTable, partitionKey);
            return true;
        }

        private bool SetupBlobRepository()
        {
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

        public async Task<bool> UpdateExperiment(Experiment experiment)
        {

            return await atExperimentRepository.Update(experiment);

        }


        public async Task<Scientist> CreateScientist(Scientist scientist)
        {
            return await atScientistRepository.Create(scientist);

        }

        public async Task<bool> UpdateScientist(Scientist scientist)
        {

            return await atScientistRepository.Update(scientist);

        }


        public async Task<Log> CreateLog(Log log)
        {

            if (log.Id == string.Empty)
                log.Id = System.Guid.NewGuid().ToString();

            if (log is TextLog 
                && (log as TextLog).TextData != null  
                && (log as TextLog).TextData.Id == string.Empty)
                (log as TextLog).TextData.Id = System.Guid.NewGuid().ToString();

            else if (log is ImageLog && (log as ImageLog).Data != null) { 
                var imageLog = log as ImageLog;
                
                if (imageLog.Data.Id == string.Empty)
                    imageLog.Data.Id = System.Guid.NewGuid().ToString();

                 imageLog.Data.ThumbnailBlobName = await blobRepository.UploadBlob(imageLog.Data.Data, imageLog.Data.Id);
                
            } else if(log is TranscriptionLog  && (log as TranscriptionLog).Data != null)
            {
                var transcriptionLog = log as TranscriptionLog;

                if (transcriptionLog.Data.Id == string.Empty)
                    transcriptionLog.Data.Id = System.Guid.NewGuid().ToString();

                transcriptionLog.Data.ThumbnailBlobName = await blobRepository.UploadBlob(transcriptionLog.Data.Data, transcriptionLog.Data.Id);

            }else 
                throw new System.NotImplementedException();

            return await atLogRepository.Create(log);

        }

        public async Task<bool> UpdateLog(Log log)
        {

            //TODO : Refactor after datatypes have been refactored
            if (log is ImageLog)
            {
                var imageLog = log as ImageLog;
                if (imageLog.Data != null)
                    imageLog.Data.ThumbnailBlobName = await blobRepository.UploadBlob(imageLog.Data.Data, imageLog.Data.Id);

            }else if (log is TranscriptionLog)
            {

                var transcriptionLog = log as TranscriptionLog;
                if (transcriptionLog.Data != null)
                    transcriptionLog.Data.ThumbnailBlobName = await blobRepository.UploadBlob(transcriptionLog.Data.Data, transcriptionLog.Data.Id);

            }
            return await atLogRepository.Update(log);

        }


        public Task<bool> DeleteExperiment(Experiment experiment)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteScientist(Scientist scientist)
        {
            throw new System.NotImplementedException();
        }

        public async  Task<bool> DeleteLog(Log log)
        {

            if (log is ImageLog)
            {
            
                var imageLog = log as ImageLog;
                if (imageLog.Data != null &&
                    ! await blobRepository.DeleteBlob(imageLog.Data.ThumbnailBlobName))
                throw new ObjectDataBaseException($"Could not delete image{imageLog.Data.Id}");

            }else if (log is TranscriptionLog)
            {
                var transcriptionLog = log as TranscriptionLog;
                if (transcriptionLog.Data != null &&
                                       ! await blobRepository.DeleteBlob(transcriptionLog.Data.ThumbnailBlobName))
                    throw new ObjectDataBaseException($"Could not delete transcription{transcriptionLog.Data.Id}");
            }
            throw new System.NotImplementedException();

        }

        bool RepositoryInterface.IsReady()
        {
            return IsReady;
        }

        public async Task<(Log[], TableContinuationToken)> GetLogsForExperiment(string experimentID, TableContinuationToken token)
        {

            
               return await atLogRepository.GetLogsForExperiment(experimentID, 100, token);
                        
           
        }

        public async Task<Log[]> GetLogsForExperiment(string experimentID)
        {
            var (logs, token) = await GetLogsForExperiment(experimentID, null);
            return logs;
        }

        public async Task<(Experiment[], TableContinuationToken)> GetAllExperiments(TableContinuationToken token)
        {
            return await atExperimentRepository.ReadOnePageAsync(100,token);
        }

        public async Task<Experiment[]> GetAllExperiments()
        {
            var (logs, token) = await GetAllExperiments(null);
            return logs;
        }
    }
}
