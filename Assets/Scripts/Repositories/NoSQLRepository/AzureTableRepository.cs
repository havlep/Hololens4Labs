
using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;




using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;//TODO look into how to do this for the whole project



namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{

    public class AzureTableRepository : MonoBehaviour
    {


        public bool IsReady { get; private set; }

        [Header("Base Settings")]

        [SerializeField]
        private string connectionString = default;
        [SerializeField]
        private string experimentName = "ExperimentDefault";

        [Header("Table Settings")]
        [SerializeField]
        private string experimentsTableName = "experiments";
        [SerializeField]
        private string scientistsTableName = "scientists";
        [SerializeField]
        private string textLogsTableName = "textLogs";
        [SerializeField]
        private string logsTableName = "logs";
        [SerializeField]
        private string textDataTableName = "textData";
        [SerializeField]
        private string partitionKey = "main";
        [SerializeField]
        private bool tryCreateTableOnStart = true;

        [Header("Blob Settings")]
        [SerializeField]
        private string blockBlobContainerName = "tracked-textLogs-thumbnails";
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
        private CloudTable textLogsTable;
        private CloudTable logsTable;
        private CloudTable textDataTable;
        private CloudTable scientistsTable;

        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        private ATExperimentRepository atExperimentRepository;
        private ATScientistRepository atScientistRepository;
        private ATLogRepository atLogRepository;

        private async void Awake()
        {
            storageAccount = CloudStorageAccount.Parse(connectionString);
            cloudTableClient = storageAccount.CreateCloudTableClient();
            experimentsTable = cloudTableClient.GetTableReference(experimentsTableName);
            scientistsTable = cloudTableClient.GetTableReference(scientistsTableName);
            textLogsTable = cloudTableClient.GetTableReference(textLogsTableName);
            textDataTable = cloudTableClient.GetTableReference(textDataTableName);
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
                    if (await textLogsTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {textLogsTableName}.");
                    }
                    if (await logsTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {logsTableName}.");
                    }
                    if (await textDataTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {textDataTableName}.");
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



            IsReady = true;
            onRepoReadyReady?.Invoke();

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

        public async Task<Experiment> CreateOrUpdateExperiment(Experiment experiment)
        {

            if (experiment.Id == string.Empty)
                await CreateExperiment(experiment);

            if (await UpdateExperiment(experiment))
                return experiment;

            return null;


        }

        public async Task<Scientist> CreateScientist(Scientist scientist)
        {
            return await atScientistRepository.Create(scientist);

        }

        public async Task<bool> UpdateScientist(Scientist scientist)
        {

            return await atScientistRepository.Update(scientist);

        }

        public async Task<Scientist> CreateOrUpdateScientist(Scientist scientist)
        {

            if (scientist.Id == string.Empty)
                await CreateScientist(scientist);

            if (await UpdateScientist(scientist))
                return scientist;

            return null;


        }

        public async Task<Log> CreateLog(Log log)
        {

            return await atLogRepository.Create(log);

        }

        public async Task<bool> UpdateLog(Log log)
        {

            return await atLogRepository.Update(log);

        }

        public async Task<Log> CreateOrUpdateLog(Log log)
        {

            if (log.Id == string.Empty)
                return await CreateLog(log);
            if (await UpdateLog(log))
                return log;
            return null;

        }
    }
}
