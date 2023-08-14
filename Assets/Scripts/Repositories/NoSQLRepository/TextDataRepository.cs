using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories
{
    public class TextDataRepository : NoSQLRepository<TextDataDTO>
    {
        public TextDataRepository(CloudTable table, string partitionKey) : base(table, partitionKey) { }

    }
}
