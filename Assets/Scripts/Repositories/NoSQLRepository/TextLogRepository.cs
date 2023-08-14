using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories
{
    public class TextLogRepository : NoSQLRepository<TextLogDTO>
    {
        public TextLogRepository(CloudTable table, string partitionKey) : base(table, partitionKey) { }

    }
}
