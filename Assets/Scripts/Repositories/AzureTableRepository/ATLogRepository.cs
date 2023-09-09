using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    public class ATLogRepository : AzureTableObjectRepository<Log, LogDTO>
    {
        public ATLogRepository(CloudTable table, string partitionKey) : base(new LogMapper(), table, partitionKey) { }

        /// <summary>
        /// Get all logs for experiment using pagination
        /// </summary>
        /// <param name="experimentID"></param>
        /// <param name="pageSize"></param>
        /// <param name="continuationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<(Log[], TableContinuationToken)> GetLogsForExperiment(string experimentID, int pageSize, TableContinuationToken continuationToken = null)
        {

            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var query = new TableQuery<LogDTO>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, experimentID))
                .Take(pageSize);

            var segment = await this.table.ExecuteQuerySegmentedAsync(query, continuationToken);
            var logs = segment.Results.Select(dto => mapper.ToOBJ(dto)).ToArray();

            return (logs, segment.ContinuationToken);
        
        }
        



    }
}
