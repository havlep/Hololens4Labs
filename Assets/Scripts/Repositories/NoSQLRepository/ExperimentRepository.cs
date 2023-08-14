using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories
{
    public class ExperimentRepository : NoSQLRepository<ExperimentDTO>
    {
        public ExperimentRepository(CloudTable table, string partitionKey) : base(table, partitionKey) { }

    }
}
