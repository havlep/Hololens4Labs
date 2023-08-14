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


using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Services.DataTransferServices;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;

namespace HoloLens4Labs.Scripts.Managers
{
    public class DataManager : MonoBehaviour
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
        private UnityEvent onDataManagerReady = default;
        [SerializeField]
        private UnityEvent onDataManagerInitFailed = default;

        private CloudStorageAccount storageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable experimentsTable;
        private CloudTable textLogsTable;
        private CloudTable logsTable;
        private CloudTable textDataTable;
        private CloudTable scientistsTable;

        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        private ExperimentTransferService experimentService;
        private ScientistTransferService scientistService;
        private TextDataTransferService textDataService;
        private TextLogTransferService textLogService;
        private LogTransferService logService;


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
                    onDataManagerInitFailed?.Invoke();
                }
            }

            
            // Setup the services that will be used to get data
            experimentService = new ExperimentTransferService(new ExperimentRepository(experimentsTable, partitionKey), new ExperimentMapper());
            scientistService = new ScientistTransferService(new ScientistRepository(scientistsTable, partitionKey), new ScientistMapper());
            textDataService = new TextDataTransferService(new TextDataRepository(textDataTable, partitionKey), new TextDataMapper());
            textLogService = new TextLogTransferService(new TextLogRepository(textLogsTable, partitionKey), new TextLogMapper());
            logService = new LogTransferService(new LogRepository(logsTable,partitionKey), new LogMapper());


            IsReady = true;
            onDataManagerReady?.Invoke();
        }

        /// <summary>
        /// Get an experiment or create one if it does not exist.
        /// </summary>
        /// <returns>Experiment instance from database.</returns>
        public async Task<ExperimentDTO> GetOrCreateExperiment()
        {
            var query = new TableQuery<ExperimentDTO>().Where(
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

            experiment = new ExperimentDTO()
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
        public async Task<bool> UpdateExperiment(ExperimentDTO experiment)
        {
            var insertOrMergeOperation = TableOperation.InsertOrMerge(experiment);
            var result = await experimentsTable.ExecuteAsync(insertOrMergeOperation);

            return result.Result != null;
        }


        /// <summary>
        /// Insert a new or update a TextLog instance on the table storage.
        /// </summary>
        /// <param name="textLog">Instance to write or update.</param>
        /// <returns>Success result.</returns>
        public async Task<bool> UploadOrUpdate(TextLogDTO textLog)
        {
            if (string.IsNullOrWhiteSpace(textLog.PartitionKey))
            {
                textLog.PartitionKey = partitionKey;
            }

            var insertOrMergeOperation = TableOperation.InsertOrMerge(textLog);
            var result = await textLogsTable.ExecuteAsync(insertOrMergeOperation);

            return result.Result != null;
        }

        /// <summary>
        /// Get all TextLog from the table.
        /// </summary>
        /// <returns>List of all TextLog from table.</returns>
        public async Task<List<TextLogDTO>> GetAllTextLogs()
        {
            var query = new TableQuery<TextLogDTO>();
            var segment = await textLogsTable.ExecuteQuerySegmentedAsync(query, null);

            return segment.Results;
        }

        /// <summary>
        /// Find a TextLogExperiment by a given Id (partition key).
        /// </summary>
        /// <param name="id">Id/Partition Key to search by.</param>
        /// <returns>Found TextLogExperiment, null if nothing is found.</returns>
        public async Task<TextLogDTO> FindTextLogById(string id)
        {
            var retrieveOperation = TableOperation.Retrieve<TextLogDTO>(partitionKey, id);
            var result = await textLogsTable.ExecuteAsync(retrieveOperation);
            var textLog = result.Result as TextLogDTO;

            return textLog;
        }

        /// <summary>
        /// Find a TextLogExperiment by its name.
        /// </summary>
        /// <param name="textLogName">Name to search by.</param>
        /// <returns>Found TextLogExperiment, null if nothing is found.</returns>
        public async Task<TextLogDTO> FindTextLogByName(string textLogName)
        {
            var query = new TableQuery<TextLogDTO>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, textLogName)));
            var segment = await textLogsTable.ExecuteQuerySegmentedAsync(query, null);

            return segment.Results.FirstOrDefault();
        }

        /// <summary>
        /// Delete a TextLogExperiment from the table.
        /// </summary>
        /// <param name="instance">Object to delete.</param>
        /// <returns>Success result of deletion.</returns>
        public async Task<bool> DeleteTextLog(TextLogDTO instance)
        {
            var deleteOperation = TableOperation.Delete(instance);
            var result = await textLogsTable.ExecuteAsync(deleteOperation);

            return result.HttpStatusCode == (int)HttpStatusCode.OK;
        }

    }
}
