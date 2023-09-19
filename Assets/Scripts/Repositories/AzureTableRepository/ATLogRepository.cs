using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    public class ATLogRepository : AzureTableObjectRepository<Log, LogDTO>
    {
        /// <summary>
        /// Concerete implementation of Azure Table object repository interface for the Log data model class and its sublcasses
        /// </summary>
        public ATLogRepository(CloudTable table, string partitionKey) : base(new LogMapper(), table, partitionKey) { }

        /// <summary>
        /// Get all Logs for Experiment using pagination
        /// </summary>
        /// <param name="experimentID">The ID of the exeriment</param>
        /// <param name="pageSize">The number of elements that should be retrieved per page</param>
        /// <param name="continuationToken">The continuation token for the pagination session</param>
        /// <returns>An array of Logs and a continuation token</returns>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<(Log[], TableContinuationToken)> GetLogsForExperiment(string experimentID, int pageSize, TableContinuationToken continuationToken = null)
        {

            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var query = new TableQuery<LogDTO>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, experimentID))
                .Take(pageSize);

            var segment = await this.table.ExecuteQuerySegmentedAsync(query, continuationToken);
            
            // Map the result to the Log type skipping over any problematic entries
            var logs = segment.Results.Aggregate( new List<Log>(), (acc, dto) => {
                try
                {
                    acc.Add(mapper.ToOBJ(dto));
                } catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                return acc;
                });

            return (logs.ToArray(), segment.ContinuationToken);
        
        }
        



    }
}
