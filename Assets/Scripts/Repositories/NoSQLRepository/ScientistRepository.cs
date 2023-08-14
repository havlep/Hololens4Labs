using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories
{
    public class ScientistRepository : NoSQLRepository<ScientistDTO>
    {
        public ScientistRepository(CloudTable table, string partitionKey) : base(table, partitionKey) { }

    }
}
