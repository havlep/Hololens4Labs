using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    public class ATLogRepository : AzureTableObjectRepository<Log, LogDTO>
    {
        public ATLogRepository(CloudTable table, string partitionKey) : base(new LogMapper(), table, partitionKey) { }

    }
}
