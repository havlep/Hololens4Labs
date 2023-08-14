using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories
{
    public class LogRepository : NoSQLRepository<LogDTO>
    {
        public LogRepository(CloudTable table, string partitionKey) : base(table, partitionKey) { }

    }
}
